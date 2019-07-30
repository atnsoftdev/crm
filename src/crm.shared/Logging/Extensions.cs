
using System;
using crm.shared;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CRM.Shared.Logging
{
    public static class Extensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string applicationName = "")
        {
            hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var appOptions = context.Configuration.GetOptions<AppOptions>("App");
                var loggingOptions = context.Configuration.GetOptions<LoggingOptions>("Logging");

                if (!Enum.TryParse<LogEventLevel>(loggingOptions.Level, true, out var level))
                {
                    level = LogEventLevel.Information;
                }
                applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;
                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);

                Configure(loggerConfiguration, level, loggingOptions);
            });

            return hostBuilder;
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, LogEventLevel level, LoggingOptions loggingOptions)
        {
            if (loggingOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console();
            }
        }
    }
}