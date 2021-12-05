using System.IO;
using System.IO.Compression;
using Logger.Data.Configuration;

namespace Logger.Utilities
{
    internal static class FileUtilities
    {
        internal static void SetDirectory()
        {
            if (!Directory.Exists(LoggerConfiguration.LogsDir))
                Directory.CreateDirectory(LoggerConfiguration.LogsDir);
        }
        
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

        internal static string GetLogFileName() => $"{LoggerUtilities.GetLogDate()}.{LoggerConfiguration.LogsExtension}";

        internal static string GetPathToLogFile(string fileName) => Path.Combine(LoggerConfiguration.LogsDir, fileName);
        
        private static void ArchiveFile(FileSystemInfo file)
        {
            var path = GetPathToLogFile(file.Name);

            using (var modFile = ZipFile.Open($"{Path.ChangeExtension(path, null)}.zip", ZipArchiveMode.Update))
            {
                modFile.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Fastest);
            }

            File.Delete(file.FullName);
        }
    }
}