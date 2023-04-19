using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using Codescovery.Library.Commons.Extensions;

namespace Codescovery.Library.DependencyInjection.Extensions
{

    public static class LoggingExtensions
    {
        public static ILoggingBuilder ConfigureSerilog(this IHostBuilder hostBuilder, ILoggingBuilder loggerBuilder)

        {
            loggerBuilder.ClearProviders();
            hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));
            return loggerBuilder;
        }
        public static ILoggingBuilder ConfigureSerilog(this IHostBuilder hostBuilder, ILoggingBuilder loggerBuilder, Action<HostBuilderContext, LoggerConfiguration> configureLogger)

        {
            if (configureLogger.IsNullOrDefault())
                return loggerBuilder;
            loggerBuilder.ClearProviders();
            hostBuilder.UseSerilog(configureLogger);
            return loggerBuilder;
        }
    }
}