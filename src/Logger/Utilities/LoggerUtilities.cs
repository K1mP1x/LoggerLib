using System;
using Logger.Data.Configuration;
using Logger.Data.Enum;
using Microsoft.Extensions.Logging;

namespace Logger.Utilities
{
    internal static class LoggerUtilities
    {
        /// <summary>
        /// Convert from (Microsoft) LogLevel to (LoggerLib) LogType
        /// </summary>
        /// <param name="logLevel">LogLevel to convert</param>
        /// <returns>Converted LogType</returns>
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

        /// <summary>
        /// Get LogType prefix
        /// </summary>
        /// <param name="logType">LogType</param>
        /// <returns>LogType prefix</returns>
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

        /// <summary>
        /// Get ConsoleColor by LogType
        /// </summary>
        /// <param name="logType">LogType</param>
        /// <returns>The color assigned to the LogType</returns>
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

        /// <summary>
        /// Write console line with color
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="color">Color</param>
        /// <param name="newLine">Print new line</param>
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

        /// <summary>
        /// Checks the settings if the log can be displayed
        /// </summary>
        /// <param name="logType">LogType</param>
        /// <returns>True if can be displayed</returns>
        internal static bool CanBeLogged(LogType logType)
        {
            if (LoggerConfiguration.LogAllLogLevels)
                return true;

            if (LoggerConfiguration.RequiredLogLevel != null)
                return logType == LoggerConfiguration.RequiredLogLevel;

            return (int) LoggerConfiguration.MinimumLogLevel <= (int) logType;
        }
        
        /// <summary>
        /// Gets the current time
        /// </summary>
        /// <returns>Time</returns>
        internal static string GetLogTime() => ParseDateTime("H:mm:ss");

        /// <summary>
        /// Gets the current date
        /// </summary>
        /// <returns>Date</returns>
        internal static string GetLogDate() => ParseDateTime("dd-MM-yyyy");
        
        /// <summary>
        /// Gets the current date/time by pattern
        /// </summary>
        /// <param name="pattern">Format pattern</param>
        /// <returns>Formatted datetime</returns>
        private static string ParseDateTime(string pattern) => string.Format($"{{0:{pattern}}}", DateTime.UtcNow);
    }
}