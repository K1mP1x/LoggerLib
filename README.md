[![ISSUES](https://img.shields.io/github/issues/KimPiks/LoggerLib)](https://github.com/KimPiks/LoggerLib/issues)
[![STARS](https://img.shields.io/github/stars/KimPiks/LoggerLib)](https://github.com/KimPiks/LoggerLib)
[![LICENSE](https://img.shields.io/github/license/KimPiks/LoggerLib)](https://github.com/KimPiks/LoggerLib/blob/main/LICENSE.txt)
[![NUGET](https://shields.io/nuget/v/loggerlib.svg)](https://www.nuget.org/packages/LoggerLib)

## General info 
Logger for your C# project<br>
Saving logs to the console and to the `.log` or `.txt` files.<br>
You can also save logs to the database (MySQL and PostgreqSQL).<br>
Old files are backed up to reduce the size of the logs.<br>

## Installation
```
PM> Install-Package LoggerLib -Version 1.2.2
```
or
```
> dotnet add package LoggerLib --version 1.2.2
```

## Usage

### Example 1
```
static void Main(string[] args)
{
    // If you want to change log directory or log extension
    LoggerConfiguration.LogsDir = "SomePath/Logs"; // Default: "logs"
    LoggerConfiguration.LogsExtension = "txt"; // Recommended: `log` or `txt` | Default: "log"
    
    // What type of logs should be logged
    LoggerConfiguration.MinimumLogLevel = LogType.Information;
    
    // Initialize logger
    Logger.Init();

    Logger.Info("Message");
    Logger.Warning("Message");
    Logger.Error("Message");
    Logger.Debug("Message");

    Logger.Info(new Exception("Exception message"));
    Logger.Warning(new Exception("Exception message"));
    Logger.Error(new Exception("Exception message"));
    Logger.Debug(new Exception("Exception message"));
}
```
#### Note
You don't need to declare `LoggerConfiguration`, values will be assigned by default.

### Example 2
If you want this to be your main logger
Example in the ASP.NET 6.0
```
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseLoggerLib();

Logger.Logger.Init();
```

### Check out the full example in the <a href="https://github.com/KimPiks/LoggerLib/tree/main/Example">Sample Application</a> in the repository

## Example output
Example file name

```
08-03-2021.log
```
Example content

```
[13:56:47][INFO] Message
[13:56:47][WARNING] Message
[13:56:47][ERROR] Message
[13:56:47][DEBUG] Message
```

Example console content
```
[15:04:15][INFO] |Microsoft.Hosting.Lifetime| - Now listening on: https://localhost:5001
[15:04:15][INFO] |Microsoft.Hosting.Lifetime| - Now listening on: http://localhost:5000
[15:04:15][INFO] |Microsoft.Hosting.Lifetime| - Application started. Press Ctrl+C to shut down.
```

## Ability to change message styles
```
// You can choose in: LogStyle.Default, LogStyle.Minimalistic, LogStyle.OneColor and LogStyle.Gray
LoggerConfiguration.LoggingStyle = LogStyle.Gray;
```

## Configuring a database connection
```
LoggerConfiguration.DbConfig = new DatabaseConfiguration()
{
    DbType = DatabaseType.MySql,
    Database = "logger",
    Username = "root",
    Password = "",
    Host = "localhost",
    Port = 3306
};
```
The table will be created automatically (Database have to be clear!)


## See also:
* ### <a href="https://kimpiks.github.io/LoggerLib/">Documentation</a>
* ### <a href="https://github.com/KimPiks/LoggerLib/blob/main/LICENSE.txt">License</a>
* ### <a href="https://github.com/KimPiks/LoggerLib/blob/main/CONTRIBUTING.md">Contributing</a>
