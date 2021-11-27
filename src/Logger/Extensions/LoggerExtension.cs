using Logger.Core;
using Microsoft.Extensions.Logging;

namespace Logger.Extensions
{
    internal static class LoggerExtension
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
    }
}