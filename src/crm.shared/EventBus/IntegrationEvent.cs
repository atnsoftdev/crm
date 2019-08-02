using System;

namespace CRM.Shared.EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
        {

        }
        public IntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}