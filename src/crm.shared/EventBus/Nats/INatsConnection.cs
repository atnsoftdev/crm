using System;
using NATS.Client;

namespace CRM.Shared.EventBus.Nats
{
    public interface INatsConnection : IDisposable
    {
        bool IsConnected { get; }
        IConnection NatsConnection { get; }
        bool TryConnect();
    }
}