using System;
using Logger.Data.Configuration;
using Logger.Data.Context;
using Logger.Data.Enum;
using Logger.Utilities;
using MySqlConnector;

namespace Logger
{
    public static class Logger
    {
        private static bool _initialized;
        
        private static LoggerContext? _context;

        /// <summary>
        /// Initialize settings
        /// </summary>
        public static void Init()
        {
            if (_initialized)
            {
                Console.WriteLine("Logger has already been initialized");
                return;
            }

            FileUtilities.SetDirectory();
            _initialized = true;

            if (LoggerConfiguration.DbConfig != null)
            {
                try
                {
                    _context = DatabaseUtilities.ConfigureDatabaseConnection();
                }
                catch (Exception e) when (e is MySqlException or ArgumentOutOfRangeException)
                {
                    Critical(e);
                }
            }
        }
        
        public static void Info(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Information, message, logToConsole, logToFile, logToDb);
        }

        public static void Info(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Information, e.Message, logToConsole, logToFile, logToDb);
        }

        public static void Warning(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Warning, message, logToConsole, logToFile, logToDb);
        }

        public static void Warning(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Warning, e.Message, logToConsole, logToFile, logToDb);
        }

        public static void Error(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Error, message, logToConsole, logToFile, logToDb);
        }

        public static void Error(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Error, e.Message, logToConsole, logToFile, logToDb);
        }

        public static void Debug(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Debug, message, logToConsole, logToFile, logToDb);
        }

        public static void Debug(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Debug, e.Message, logToConsole, logToFile, logToDb);
        }

        public static void Critical(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Critical, message, logToConsole, logToFile, logToDb);
        }

        public static void Critical(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            Log(LogType.Critical, e.Message, logToConsole, logToFile, logToDb);
        }

        internal static void Log(LogType logType, string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false)
        {
            if (!_initialized)
            {
                Console.WriteLine("The logger must be initialized");
                return;
            }

            if (!LoggerUtilities.CanBeLogged(logType)) return;

            Core.Core.Log(logType, message, logToConsole, logToFile, logToDb, _context);
        }
    }
}