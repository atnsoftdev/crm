using System;
using System.Text;
using Microsoft.Extensions.Logging;
using NATS.Client;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM.Shared.EventBus.Nats
{
    public class EventBusNats : IEventBus, IDisposable
    {
        private readonly ILogger<EventBusNats> _logger;
        private readonly INatsConnection _connection;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly IServiceProvider _serviceProvider;

        public EventBusNats(INatsConnection connection, ILoggerFactory loggerFactory,
            IEventBusSubscriptionsManager subsManager,
            IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<EventBusNats>();
            _connection = connection;
            _subsManager = subsManager;
            _serviceProvider = serviceProvider;
        }
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            DoInternalSubscription(eventName);

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _subsManager.AddSubscription<T, TH>();
        }

        public void Dispose()
        {
            _subsManager.Clear();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_connection.IsConnected)
                {
                    _connection.TryConnect();
                }
                var sub = _connection.NatsConnection.SubscribeAsync(eventName);
                sub.MessageHandler += Consumer_Received();
                sub.Start();
            }
        }

        private EventHandler<MsgHandlerEventArgs> Consumer_Received()
        {
            return async (sender, args) =>
            {
                var eventName = args.Message.Subject;
                var msg = Encoding.UTF8.GetString(args.Message.Data);
                try
                {
                    await ProcessEvent(eventName, msg);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", msg);
                }
            };
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing NATS event: {EventName}", eventName);
            var subscriptions = _subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {

                }
                else
                {
                    var handler = _serviceProvider.GetService(subscription.HandlerType);
                    if (handler == null)
                    {
                        continue;
                    }
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }
    }
}