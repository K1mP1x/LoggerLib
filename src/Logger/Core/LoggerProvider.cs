using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Logger.Core
{
    internal class LoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, LoggerFactory> _loggers = new();

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new LoggerFactory(name));

        public void Dispose() => _loggers.Clear();
    }
}