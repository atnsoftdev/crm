using System.Threading.Tasks;
using CRM.Lead.Api.IntegrationEvents.Events;
using CRM.Shared.EventBus;
using Microsoft.Extensions.Logging;

namespace CRM.Lead.Api.IntegrationEvents.Handling
{
    public class LeadCreatedEventHandler : IIntegrationEventHandler<LeadCreatedEvent>
    {
        private readonly ILogger<LeadCreatedEventHandler> _logger;
        public LeadCreatedEventHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LeadCreatedEventHandler>();
        }
        public async Task Handle(LeadCreatedEvent @event)
        {
            _logger.LogInformation(@event.Id.ToString());
            await Task.FromResult(0);
        }
    }
}