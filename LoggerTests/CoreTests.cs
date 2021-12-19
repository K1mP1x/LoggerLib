using System;
using System.IO;
using FluentAssertions;
using Logger.Data.Configuration;
using Xunit;

namespace LoggerTests
{
    public class CoreTests
    {
        public CoreTests()
        {
            LoggerConfiguration.LogsDir = TestsSettings.LogsDir;
            LoggerConfiguration.LogsExtension = TestsSettings.LogsExtension;
            Logger.Logger.Init();
        }
        
        [Fact]
        public void FileLog_WhenLog_CreatesLogFile()
        {
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            
            Logger.Logger.Info("test");

            File.Exists(Path.Combine(TestsSettings.LogsDir, $"{date}.{TestsSettings.LogsExtension}")).Should().BeTrue();
        }
        
        [Theory]
        [InlineData("Test message")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("87132")]
        [InlineData("][\''././,,")]
        [InlineData("░Æÿ~")]
        [InlineData("")]
        [InlineData(" ")]
        public void ConsoleLog_WhenCalledLogMethod_ReturnsLogMessage(string logMessage)
        {
            var time = DateTime.Now.ToString("H:mm:ss");
            var output = new StringWriter();
            Console.SetOut(output);

            Logger.Logger.Info(logMessage);
            var outputLog = output.ToString().Replace(Environment.NewLine, string.Empty);

            outputLog.Should().Be($"[{time}][INFO] {logMessage}");
        }
    }
}