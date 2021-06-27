using System;
using System.IO;
using LoggerLib.Data;

namespace LoggerLib.Utilities
{
    internal static class FileUtilities
    {
        public static string GetLogFileName()
        {
            return $"{GetLogDate()}.{LoggerConfig.LogsExtension}";
        }

        public static string GetPathToLogFile(string fileName)
        {
            return Path.Combine(LoggerConfig.LogsDir, fileName);
        }

        public static string GetLogTime()
        {
            return ParseDateTime("H:mm:ss");
        }

        public static string GetLogDate()
        {
            return ParseDateTime("dd-MM-yyyy");
        }

        private static string ParseDateTime(string pattern)
        {
            return string.Format($"{{0:{pattern}}}", DateTime.Now);
        }
    }
}
