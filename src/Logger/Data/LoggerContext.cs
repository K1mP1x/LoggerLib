using System;
using Microsoft.EntityFrameworkCore;

namespace Logger.Data
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
                    v => (LogType)Enum.Parse(typeof(LogType), v));
        }
    }
}