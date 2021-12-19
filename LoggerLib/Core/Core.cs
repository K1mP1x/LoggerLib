using System;
using System.IO;
using System.IO.Compression;
using LoggerLib.Data;
using LoggerLib.Utilities;

namespace LoggerLib.Core
{
    internal class Core
    {
        private static object _saveLock;

        public Core() =>
            _saveLock = new object();

        public void Log(LogType logType, string message)
        {
            this.SetDirectory();
            this.CheckOldFiles();

            var fileName = FileUtilities.GetLogFileName();
            var prefix = LoggerUtilities.GetLogPrefix(logType);
            var logPath = FileUtilities.GetPathToLogFile(fileName);
            var time = $"[{FileUtilities.GetLogTime()}]";

            var originalColor = Console.ForegroundColor;
            var prefixColor = LoggerUtilities.GetLogPrefixColor(logType);

            lock (_saveLock)
            {
                this.WriteConsoleLog(originalColor, prefixColor, time, prefix, message);

                var sw = new StreamWriter(logPath, true);
                sw.WriteLine($"{time}{prefix} {message}");
                sw.Close();
            }
        }

        private void WriteConsoleLog(ConsoleColor originalColor, ConsoleColor prefixColor,
            string time, string prefix, string message)
        {
            Console.ForegroundColor = originalColor;
            Console.Write(time);

            Console.ForegroundColor = prefixColor;
            Console.Write($"{prefix} ");

            Console.ForegroundColor = originalColor;
            Console.WriteLine($"{message}");
        }

        private void SetDirectory()
        {
            if (!Directory.Exists(LoggerConfig.LogsDir))
                Directory.CreateDirectory(LoggerConfig.LogsDir);
        }

        private void CheckOldFiles()
        {
            var d = new DirectoryInfo(LoggerConfig.LogsDir);

            foreach (var file in d.GetFiles($"*.{LoggerConfig.LogsExtension}"))
            {
                var date = FileUtilities.GetLogDate();

                if (!file.Name.Equals($"{date}.{LoggerConfig.LogsExtension}"))
                {
                    this.ArchiveFile(file);
                }
            }
        }

        private void ArchiveFile(FileSystemInfo file)
        {
            var path = FileUtilities.GetPathToLogFile(file.Name);

            using (var modFile = ZipFile.Open($"{Path.ChangeExtension(path, null)}.zip", ZipArchiveMode.Update))
            {
                modFile.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Fastest);
            }

            File.Delete(file.FullName);
        }
    }
}
