using Logger.Data;

// Set logging style
// You can choose in: LogStyle.Default, LogStyle.Minimalistic, LogStyle.OneColor and LogStyle.Gray
LoggerConfiguration.LoggingStyle = LogStyle.Gray;

// If you want to change log directory or log extension
LoggerConfiguration.LogsDir = "logs"; // Default: "logs"
LoggerConfiguration.LogsExtension = "txt"; // Recommended: `log` or `txt` | Default: "log"

// What type of logs should be logged
LoggerConfiguration.MinimumLogLevel = LogType.Information;

// If you want to log into the database, you have to configure the connection
// Currently the logger only supports MySQL
LoggerConfiguration.DbConfig = new DatabaseConfiguration()
{
    Database = "logger",
    Username = "root",
    Password = "",
    Host = "localhost",
    Port = 3306
};

// Initialize logger
// Each configuration should be placed before this method
Logger.Logger.Init();

// Example log
// Logger.Info(message, logToConsole, logToFile, logToDb);
Logger.Logger.Info("asd", true, true, true);