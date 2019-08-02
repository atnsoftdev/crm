using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client;
using Polly;
using Polly.Retry;

namespace CRM.Shared.EventBus.Nats
{
    public class DefaultNatsConnection : INatsConnection
    {
        private readonly ILogger<DefaultNatsConnection> _logger;
        private readonly NatsOptions _options;
        private IConnection _connection;
        private bool _disposed;
        object sync_root = new object();

        public DefaultNatsConnection(ILoggerFactory loggerFactory, IOptions<NatsOptions> options)
        {
            _logger = loggerFactory.CreateLogger<DefaultNatsConnection>();
            _options = options.Value;
        }
        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.State == ConnState.CONNECTED && !_disposed;
            }
        }

        public IConnection NatsConnection => _connection;

        public bool TryConnect()
        {
            lock (sync_root)
            {
                if (!IsConnected)
                {
                    _logger.LogInformation("Nats client is trying to connect");

                    var policy = RetryPolicy.Handle<NATSNoServersException>()
                        .Or<NATSConnectionException>()
                        .WaitAndRetry(5, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)), (ex, time) =>
                        {
                            _logger.LogWarning(ex, "NATS Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                        });

                    policy.Execute(() =>
                    {
                        _connection = new ConnectionFactory().CreateConnection(_options.Url);
                    });
                }

                return IsConnected;
            }
        }

        public void Dispose()
        {
            _logger.LogInformation("NATS client is trying to disconnect");
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            try
            {
                _connection.Close();
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    }
}