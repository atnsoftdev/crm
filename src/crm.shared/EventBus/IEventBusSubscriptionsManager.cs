using System;
using System.Collections.Generic;

namespace CRM.Shared.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        string GetEventKey<T>();
        void Clear();
        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;
        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;
        event EventHandler<string> OnEventRemoved;
        bool HasSubscriptionsForEvent(string eventName);
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
    }
}