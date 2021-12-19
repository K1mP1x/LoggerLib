## Documentation

### General information
* Currently only MySQL is supported
* All configuration data must be declared before logger initialization
* You can see usage examples in the `#QuickStart` section, in `README.md` in the library repository, and in the `ExampleApp` project

### Quick start
```
using Logger.Data.Configuration;
using Logger.Data.Enum;

LoggerConfiguration.LoggingStyle = LogStyle.Gray;

LoggerConfiguration.LogsDir = "logs"; 
LoggerConfiguration.LogsExtension = "txt"; 

LoggerConfiguration.MinimumLogLevel = LogType.Information;

LoggerConfiguration.DbConfig = new DatabaseConfiguration()
{
    Database = "logger",
    Username = "root",
    Password = "",
    Host = "localhost",
    Port = 3306
};

Logger.Logger.Init();

Logger.Logger.Info("asd", true, true, true);
```

### Log types
* Trace
* Debug
* Information
* Warning
* Error
* Critical
* None

#### Using example
```
LogType.Information
```

### Log styles
* Default
* Gray
* OneColor
* Minimalistic

#### Using example
```
LogStyle.Gray
```

### Configuration

* Login style setting (differ in format or color)
```
LoggerConfiguration.LoggingStyle = LogStyle.Gray;
```

* Log path change
```
LoggerConfiguration.LogsDir = "logs"; // Default: "logs"
```

* Log extension change
```
LoggerConfiguration.LogsExtension = "txt"; // Default: "log"
```

* Minimum log type registered
```
LoggerConfiguration.MinimumLogLevel = LogType.Information;
```

* Require a specific type of logs
```
LoggerConfiguration.RequiredLogLevel = LogType.Critical;
```

* Whether all log types are to be displayed
```
LoggerConfiguration.LogAllLogLevels = true;
```

* Database configuration (currently only MySQL is supported)
```
LoggerConfiguration.DbConfig = new DatabaseConfiguration()
{
    Database = "logger",
    Username = "root",
    Password = "",
    Host = "localhost",
    Port = 3306
};
```

* Initializing
Logger.Init();


### Logging

* Available methods
```
Logger.Info(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Warning(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Error(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Debug(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Critical(string message, bool logToConsole = true, bool logToFile = true, bool logToDb = false);

Logger.Info(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Warning(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Error(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Debug(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
Logger.Critical(Exception e, bool logToConsole = true, bool logToFile = true, bool logToDb = false);
```

