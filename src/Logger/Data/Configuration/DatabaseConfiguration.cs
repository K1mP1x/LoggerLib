namespace Logger.Data.Configuration
{
    public class DatabaseConfiguration
    {
        public DatabaseType DbType { get; set; } = DatabaseType.MySql;
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public uint Port { get; set; }
    }

    public enum DatabaseType
    {
        MySql
        // SqlServer - Soon
    }
}