using LoggerLib.Core;
using LoggerLib.Data;
using Microsoft.Extensions.Logging;

namespace LoggerLib.Extensions
{
    public static class LoggerExtension
    {
        public static ILoggingBuilder AddLoggerBuilder(
            this ILoggingBuilder builder,
            LoggerFactoryConfiguration config)
        {
            builder.AddProvider(new LoggerProvider(CheckConfig(config)));
            return builder;
        }

        public static ILoggerFactory AddLoggerFactory(
            this ILoggerFactory factory,
            LoggerFactoryConfiguration config)
        {
            factory.AddProvider(new LoggerProvider(CheckConfig(config)));
            return factory;
        }

        public static ILoggingBuilder AddLoggerBuilder(
            this ILoggingBuilder builder)
        {
            builder.AddProvider(new LoggerProvider(new LoggerFactoryConfiguration()));
            return builder;
        }

        public static ILoggerFactory AddLoggerFactory(
            this ILoggerFactory factory)
        {
            factory.AddProvider(new LoggerProvider(new LoggerFactoryConfiguration()));
            return factory;
        }

        private static LoggerFactoryConfiguration CheckConfig(LoggerFactoryConfiguration config)
        {
            return config ?? new LoggerFactoryConfiguration();
        }
    }
}
