namespace LoggerLib.Data
{
    public class LoggerFactoryConfiguration
    {
        public LogType? RequiredLogLevel { get; set; } = null;

        public bool LogAllLogLevels { get; set; } = false;

        public LogType MinimumLogLevel { get; set; } = LogType.Debug;
    }
}
