using System.IO;
using System.IO.Compression;
using Logger.Data.Configuration;

namespace Logger.Utilities
{
    internal static class FileUtilities
    {
        /// <summary>
        /// Create logs directory if not exists
        /// </summary>
        internal static void SetDirectory()
        {
            if (!Directory.Exists(LoggerConfiguration.LogsDir))
                Directory.CreateDirectory(LoggerConfiguration.LogsDir);
        }
        
        /// <summary>
        /// Archive old logs
        /// </summary>
        internal static void CheckOldFiles()
        {
            var d = new DirectoryInfo(LoggerConfiguration.LogsDir);

            foreach (var file in d.GetFiles($"*.{LoggerConfiguration.LogsExtension}"))
            {
                var date = LoggerUtilities.GetLogDate();

                if (!file.Name.Equals($"{date}.{LoggerConfiguration.LogsExtension}"))
                {
                    ArchiveFile(file);
                }
            }
        }

        /// <summary>
        /// Archive logs to *.zip
        /// </summary>
        /// <param name="file">File info</param>
        private static void ArchiveFile(FileSystemInfo file)
        {
            var path = GetPathToLogFile(file.Name);

            using (var modFile = ZipFile.Open($"{Path.ChangeExtension(path, null)}.zip", ZipArchiveMode.Update))
            {
                modFile.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Fastest);
            }

            File.Delete(file.FullName);
        }
        
        /// <summary>
        /// Get log file name by current date
        /// </summary>
        /// <returns>File name</returns>
        internal static string GetLogFileName() => $"{LoggerUtilities.GetLogDate()}.{LoggerConfiguration.LogsExtension}";

        
        /// <summary>
        /// Get path to log file
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Path to log file</returns>
        internal static string GetPathToLogFile(string fileName) => Path.Combine(LoggerConfiguration.LogsDir, fileName);
    }
}