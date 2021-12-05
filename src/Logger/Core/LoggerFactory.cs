using System;
using Logger.Data;
using Logger.Utilities;
using Microsoft.Extensions.Logging;

namespace Logger.Core
{
    internal class LoggerFactory : ILogger
    {
        private readonly string _name;

        public LoggerFactory(
            string name) =>
            _name = name;

        public IDisposable BeginScope<TState>(TState state) => default!;

        [Obsolete("This method is implemented in the LoggerUtilities class as CanBeLogged(LogType);")]
        public bool IsEnabled(LogLevel logLevel) =>
            throw new NotImplementedException();

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var logType = LoggerUtilities.ConvertLogType(logLevel);
            Logger.Log(logType, $"|{_name}| - {formatter(state, exception)}");
        }
    }
}