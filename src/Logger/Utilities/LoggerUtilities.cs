using System;
using Logger.Data;
using Microsoft.Extensions.Logging;

namespace Logger.Utilities
{
    internal static class LoggerUtilities
    {
        internal static LogType ConvertLogType(LogLevel logLevel)
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

        internal static string GetLogPrefix(LogType logType)
        {
            return logType switch
            {
                LogType.Warning => "WARNING",
                LogType.Error => "ERROR",
                LogType.Debug => "DEBUG",
                LogType.Trace => "TRACE",
                LogType.Critical => "CRITICAL",
                LogType.Information => "INFO",
                _ => ""
            };
        }

        internal static ConsoleColor GetLogPrefixColor(LogType logType)
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

        internal static void ConsoleColorWrite(string message, ConsoleColor color, bool newLine = false)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            
            Console.ForegroundColor = originalColor;
        }

        internal static bool CanBeLogged(LogType logType)
        {
            if (LoggerConfiguration.LogAllLogLevels)
                return true;

            if (LoggerConfiguration.RequiredLogLevel != null)
                return logType == LoggerConfiguration.RequiredLogLevel;

            return (int) LoggerConfiguration.MinimumLogLevel <= (int) logType;
        }
        
        internal static string GetLogTime() => ParseDateTime("H:mm:ss");

        internal static string GetLogDate() => ParseDateTime("dd-MM-yyyy");
        
        private static string ParseDateTime(string pattern) => string.Format($"{{0:{pattern}}}", DateTime.Now);
    }
}