using System;
using System.Linq;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Hosting;

namespace CRM.Shared.Vault
{
    public static class Extensions
    {
        public static IHostBuilder UseVault(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                var options = cfg.Build().GetOptions<VaultOptions>("Vault");
                var enabled = options.Enabled;
                var vaultEnabled = Environment.GetEnvironmentVariable("VAULT_ENABLED")?.ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(vaultEnabled))
                {
                    enabled = vaultEnabled == "true" || vaultEnabled == "1";
                }
                if (enabled)
                {
                    var client = new VaultStore(options);
                    var secret = client.GetDefaultAsync().GetAwaiter().GetResult();
                   
                    var source = new MemoryConfigurationSource()
                    {
                        InitialData = secret.ToDictionary(k => k.Key,  p=>p.Value.ToString())
                    };
                    cfg.Add(source);
                }

            });
            return hostBuilder;
        }
    }
}