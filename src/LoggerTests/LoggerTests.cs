using System.IO;
using FluentAssertions;
using Logger.Data.Configuration;
using Xunit;

namespace LoggerTests
{
    public class LoggerTests
    {
        static LoggerTests()
        {
            if (Directory.Exists(TestsSettings.LogsDir))
                Directory.Delete(TestsSettings.LogsDir, true);
        }
        
        public LoggerTests()
        {
            LoggerConfiguration.LogsDir = TestsSettings.LogsDir;
            LoggerConfiguration.LogsExtension = TestsSettings.LogsExtension;
            Logger.Logger.Init();
        }
        
        [Fact]
        public void Init_WhenInitialize_CreatesLogDirectory()
        {
            Directory.Exists(TestsSettings.LogsDir).Should().BeTrue();
        }
    }
}