[![ISSUES](https://img.shields.io/github/issues/K1mP1x/LoggerLib)](https://github.com/K1mP1x/LoggerLib/issues)
[![STARS](https://img.shields.io/github/stars/K1mP1x/LoggerLib)](https://github.com/K1mP1x/LoggerLib)
[![LICENSE](https://img.shields.io/github/license/K1mP1x/LoggerLib)](https://github.com/K1mP1x/LoggerLib/blob/main/LICENSE.txt)
[![NUGET](https://shields.io/nuget/v/loggerlib.svg)](https://www.nuget.org/packages/LoggerLib)

## General info 
Logger for your C# project<br>
Saving logs to the console and to `.log` or `.txt` files.<br>
Old files are backed up to reduce the size of the logs.<br>
The library is currently only compatible with .NET 5.0 projects

## Requirements
* .NET 5.0

## Instalation
```
PM> Install-Package LoggerLib -Version 1.0.3
```
or
```
> dotnet add package LoggerLib --version 1.0.3
```

## Usage

### Example 1
```
static void Main(string[] args)
{
    // If you want to change log directory or log extension
    LoggerConfig.LogsDir = "SomePath/Logs"; // Default: "logs"
    LoggerConfig.LogsExtension = "txt"; // Recommended: `log` or `txt` | Default: "log"
    
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
You don't need to declare the `LogsDir` and `LogsExtension` values. The default values will then be set.

### Example 2
If you want this to be your main logger, make these changes.
```
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureLogging(config =>
        {
            config.ClearProviders();
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
        
--------------------------------------------------------------
// In `ConfigureServices`

services.AddLogging(loggingBuilder =>
    loggingBuilder.AddLoggerBuilder(new LoggerFactoryConfiguration
    {
        RequiredLogLevel = LogLevel.Information
    }));
```

### Example `LoggerFactoryConfiguration`
```
new LoggerFactoryConfiguration
{
    RequiredLogLevel = LogLevel.Information,
    MinimumLogLevel = LogLevel.Trace,
    LogAllLogLevels = false
}
```

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

## License
See LICENSE.txt

## Contributing
1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

