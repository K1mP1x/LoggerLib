using Logger.Data.Configuration;
using Logger.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Logger.Utilities
{
    internal static class DatabaseUtilities
    {
        /// <summary>
        /// Configure connection to the database
        /// </summary>
        /// <returns>LoggerContext instance</returns>
        /// <exception cref="ArgumentOutOfRangeException">Non supported database type</exception>
        internal static LoggerContext? ConfigureDatabaseConnection()
        {
            var services = new ServiceCollection();
            var connectionString = "";

            switch (LoggerConfiguration.DbConfig!.DbType) 
            {
                case DatabaseType.MySql: 
                    connectionString = GetMysqlConnectionString(LoggerConfiguration.DbConfig); 
                    services.AddDbContext<LoggerContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); 
                    break;
                case DatabaseType.PostgreSQL:
                    connectionString = GetPostgreSQLConnectionString(LoggerConfiguration.DbConfig);
                    services.AddDbContext<LoggerContext>(options => options.UseNpgsql(connectionString));
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(); 
            }
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<LoggerContext>(); // To create logs table
        }

        /// <summary>
        /// Generate MySQL connection string
        /// </summary>
        /// <param name="config">Database configuration</param>
        /// <returns>Connection string</returns>
        private static string GetMysqlConnectionString(DatabaseConfiguration config)
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

        /// <summary>
        /// Generate PostgreSQL connection string
        /// </summary>
        /// <param name="config">Database configuration</param>
        /// <returns>Connection string</returns>
        private static string GetPostgreSQLConnectionString(DatabaseConfiguration config)
        {
            return $"Host={config.Host};" +
                $"Database={config.Database};" +
                $"Username={config.Username};" +
                $"Password={config.Password};" +
                $"Port={config.Port};";
        }
    }
}