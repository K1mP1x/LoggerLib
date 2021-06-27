using System;
using LoggerLib.Data;

namespace LoggerLib
{
    public class Logger
    {
        private static bool _initialized;

        private static Core.Core _core;

        public static void Init()
        {
            if (_initialized)
            {
                Console.WriteLine("Logger has already been initialized");
                return;
            }

            _core = new Core.Core();
            _initialized = true;
        }


        public static void Info(string message)
        {
            Log(LogType.Information, message);
        }

        public static void Info(Exception e)
        {
            Log(LogType.Information, e.Message);
        }

        public static void Warning(string message)
        {
            Log(LogType.Warning, message);
        }

        public static void Warning(Exception e)
        {
            Log(LogType.Warning, e.Message);
        }

        public static void Error(string message)
        {
            Log(LogType.Error, message);
        }

        public static void Error(Exception e)
        {
            Log(LogType.Error, e.Message);
        }

        public static void Debug(string message)
        {
            Log(LogType.Debug, message);
        }

        public static void Debug(Exception e)
        {
            Log(LogType.Debug, e.Message);
        }

        public static void Critical(string message)
        {
            Log(LogType.Critical, message);
        }

        public static void Critical(Exception e)
        {
            Log(LogType.Critical, e.Message);
        }

        internal static void Log(LogType logType, string message)
        {
            if (!_initialized)
            {
                Console.WriteLine("The logger must be initialized");
                return;
            }

            _core.Log(logType, message);
        }
    }
}
