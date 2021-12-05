using Logger.Data.Enum;
using Logger.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Logger.Data.Context
{
    public class LoggerContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options) => this.Database.EnsureCreated();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Log>()
                .Property(e => e.LogType)
                .HasConversion(
                    v => v.ToString(),
                    v => (LogType)System.Enum.Parse(typeof(LogType), v));
        }
    }
}