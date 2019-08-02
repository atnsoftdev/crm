namespace CRM.Shared.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;
    }
}