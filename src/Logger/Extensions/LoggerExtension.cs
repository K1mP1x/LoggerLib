using System;
using Logger.Core;
using Logger.Data.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logger.Extensions
{
    public static class LoggerExtension
    {
        internal static ILoggingBuilder AddLoggerBuilder(
            this ILoggingBuilder builder)
        {
            builder.AddProvider(new LoggerProvider());
            return builder;
        }

        internal static ILoggerFactory AddLoggerFactory(
            this ILoggerFactory factory)
        {
            factory.AddProvider(new LoggerProvider());
            return factory;
        }

        public static IHostBuilder UseLoggerLib(this IHostBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(services =>
            {
                services.AddLogging(loggingBuilder => loggingBuilder.AddLoggerBuilder());
            });

            return builder;
        }
    }
}