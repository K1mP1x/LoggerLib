using System;
using Logger.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Logger.Utilities
{
    internal static class DatabaseUtilities
    {
        internal static LoggerContext? ConfigureDatabaseConnection()
        {
            var services = new ServiceCollection();

            switch (LoggerConfiguration.DbConfig!.DbType) 
            { 
                case DatabaseType.MySql: 
                    var connectionString = DatabaseUtilities.GetMysqlConnectionString(LoggerConfiguration.DbConfig); 
                    services.AddDbContext<LoggerContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); 
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(); 
            }
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<LoggerContext>(); // To create logs table
        }
        
        internal static string GetMysqlConnectionString(DatabaseConfiguration config)
        {
            return new MySqlConnectionStringBuilder()
            {
                Server = config.Host,
                UserID = config.Username,
                Password = config.Password,
                Database = config.Database,
                Port = config.Port
            }.ToString();
        }
    }
}