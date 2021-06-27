using System;
using LoggerLib.Data;
using Microsoft.Extensions.Logging;

namespace LoggerLib.Utilities
{
    internal static class LoggerUtilities
    {
        public static LogType ConvertLogType(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => LogType.Trace,
                LogLevel.Debug => LogType.Debug,
                LogLevel.Information => LogType.Information,
                LogLevel.Warning => LogType.Warning,
                LogLevel.Error => LogType.Error,
                LogLevel.Critical => LogType.Critical,
                LogLevel.None => LogType.None,
                _ => LogType.Information
            };
        }

        public static string GetLogPrefix(LogType logType)
        {
            return logType switch
            {
                LogType.Warning => "[WARNING]",
                LogType.Error => "[ERROR]",
                LogType.Debug => "[DEBUG]",
                LogType.Trace => "[TRACE]",
                LogType.Critical => "[CRITICAL]",
                LogType.Information => "[INFO]",
                _ => ""
            };
        }

        public static ConsoleColor GetLogPrefixColor(LogType logType)
        {
            return logType switch
            {
                LogType.Warning => ConsoleColor.Yellow,
                LogType.Error => ConsoleColor.Red,
                LogType.Debug => ConsoleColor.DarkMagenta,
                LogType.Trace => ConsoleColor.Magenta,
                LogType.Critical => ConsoleColor.DarkRed,
                LogType.Information => ConsoleColor.Green,
                _ => ConsoleColor.DarkGray
            };
        }
    }
}
