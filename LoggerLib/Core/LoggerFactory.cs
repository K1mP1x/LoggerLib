using System;
using LoggerLib.Data;
using LoggerLib.Utilities;
using Microsoft.Extensions.Logging;

namespace LoggerLib.Core
{
    internal class LoggerFactory : ILogger
    {
        private readonly string _name;
        private readonly LoggerFactoryConfiguration _config;

        public LoggerFactory(
            string name,
            LoggerFactoryConfiguration config) =>
            (_name, _config) = (name, config);

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            var logType = LoggerUtilities.ConvertLogType(logLevel);
            if (_config.LogAllLogLevels)
                return true;

            if (_config.RequiredLogLevel != null)
                return logType == _config.RequiredLogLevel;

            return (int) _config.MinimumLogLevel <= (int) logType;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var logType = LoggerUtilities.ConvertLogType(logLevel);
            if (!IsEnabled(logLevel)) return;

            Logger.Log(logType, $"|{_name}| - {formatter(state, exception)}");
        }
    }
}
