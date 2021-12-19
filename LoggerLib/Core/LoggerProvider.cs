using System.Collections.Concurrent;
using LoggerLib.Data;
using Microsoft.Extensions.Logging;

namespace LoggerLib.Core
{
    sealed class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerFactoryConfiguration _config;
        private readonly ConcurrentDictionary<string, LoggerFactory> _loggers = new();

        public LoggerProvider(LoggerFactoryConfiguration config) =>
            _config = config;

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LoggerFactory(name, _config));

        public void Dispose() => _loggers.Clear();
    }
}
