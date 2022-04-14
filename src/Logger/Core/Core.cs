using System;
using System.IO;
using Logger.Data.Configuration;
using Logger.Data.Context;
using Logger.Data.Enum;
using Logger.Data.Model;
using Logger.Utilities;
using MySqlConnector;

namespace Logger.Core
{
    internal static class Core
    {
        private static readonly object? SaveLock = new ();

        /// <summary>
        /// Logging method
        /// </summary>
        /// <param name="logType">LogType</param>
        /// <param name="message">Log message</param>
        /// <param name="logToConsole">Whether the log is to be sent to the console</param>
        /// <param name="logToFile">Whether the log is to be sent to the file</param>
        /// <param name="logToDb">Whether the log is to be sent to the database</param>
        /// <param name="dbContext">DbContext when using a database</param>
        public static void Log(LogType logType, string message, bool logToConsole, bool logToFile, bool logToDb, LoggerContext? dbContext)
        {
            FileUtilities.CheckOldFiles();

            lock (SaveLock!)
            {
                var logDate = LoggerUtilities.GetLogDate();
                var logTime = LoggerUtilities.GetLogTime();

                try
                {
                    if (logToConsole) ConsoleLog(message, logTime, logType);
                    if (logToFile) FileLog(message, logTime, logType);
                    if (logToDb && dbContext != null) DatabaseLog(message, $"{logDate} {logTime}", logType, dbContext);
                }
                catch (Exception e) when (e is MySqlException or IOException or InvalidOperationException)
                {
                    Logger.Critical(e);
                }
            }
        }

        /// <summary>
        /// Log to the console
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="time">Log time</param>
        /// <param name="logType">LogType</param>
        /// <exception cref="ArgumentOutOfRangeException">Unsupported LogStyle</exception>
        private static void ConsoleLog(string message, string time, LogType logType)
        {
            var prefix = LoggerUtilities.GetLogPrefix(logType);
            
            switch (LoggerConfiguration.LoggingStyle)
            {
                case LogStyle.Gray:
                    LoggerUtilities.ConsoleColorWrite("[", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite(time, ConsoleColor.Gray);
                    LoggerUtilities.ConsoleColorWrite("]", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite("[", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite(prefix, ConsoleColor.Gray);
                    LoggerUtilities.ConsoleColorWrite("]", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite($" {message}", Console.ForegroundColor, true);
                    break;
                
                case LogStyle.OneColor:
                    Console.WriteLine($"[{time}][{prefix}] {message}");
                    break;
                
                case LogStyle.Minimalistic:
                    LoggerUtilities.ConsoleColorWrite(time, ConsoleColor.Gray);
                    LoggerUtilities.ConsoleColorWrite($" {prefix}", ConsoleColor.Gray);
                    LoggerUtilities.ConsoleColorWrite($" {message}", Console.ForegroundColor, true);
                    break;
                
                case LogStyle.Default:
                    var prefixColor = LoggerUtilities.GetLogPrefixColor(logType);
                    
                    LoggerUtilities.ConsoleColorWrite("[", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite(time, ConsoleColor.Gray);
                    LoggerUtilities.ConsoleColorWrite("]", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite("[", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite(prefix, prefixColor);
                    LoggerUtilities.ConsoleColorWrite("]", ConsoleColor.DarkGray);
                    LoggerUtilities.ConsoleColorWrite($" {message}", Console.ForegroundColor, true);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Log to the file
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="time">Log message</param>
        /// <param name="logType">LogType</param>
        private static void FileLog(string message, string time, LogType logType)
        {
            var fileName = FileUtilities.GetLogFileName();
            var logPath = FileUtilities.GetPathToLogFile(fileName);
            var prefix = LoggerUtilities.GetLogPrefix(logType);
            
            var sw = new StreamWriter(logPath, true);
            
            switch (LoggerConfiguration.LoggingStyle)
            {
                case LogStyle.Minimalistic:
                    sw.WriteLine($"{time} {prefix} {message}");
                    break;
                
                default:
                    sw.WriteLine($"[{time}][{prefix}] {message}");
                    break;
            }
            
            sw.Close();
        }

        /// <summary>
        /// Log to the database
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="time">Log time</param>
        /// <param name="logType">LogType</param>
        /// <param name="context">DbContext</param>
        private static void DatabaseLog(string message, string time, LogType logType, LoggerContext context)
        {
            if (!context.Database.CanConnect())
            {
                Logger.Error("A connection to the database could not be established");
                return;
            }
            
            var log = new Log()
            {
                LogType = logType,
                DateTime = DateTime.Parse(time).ToUniversalTime(),
                Message = message
            };

            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}