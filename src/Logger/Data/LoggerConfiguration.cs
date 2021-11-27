namespace Logger.Data
{
    public static class LoggerConfiguration
    {
        public static string LogsDir { get; set; } = "logs";

        public static string LogsExtension { get; set; } = "log";
        
        public static LogType? RequiredLogLevel { get; set; } = null;

        public static bool LogAllLogLevels { get; set; } = false;

        public static LogType MinimumLogLevel { get; set; } = LogType.Debug;

        public static LogStyle LoggingStyle { get; set; } = LogStyle.Default;

        public static DatabaseConfiguration? DbConfig { get; set; } = null;
    }
}