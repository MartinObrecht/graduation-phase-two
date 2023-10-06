using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using TechBlog.NewsManager.API.Domain.Logger;

namespace TechBlog.NewsManager.API.Infrastructure.Logger
{
    [ExcludeFromCodeCoverage]
    public class ApplicationInsightsLogger : ILoggerManager
    {
        private readonly LoggerManagerSeverity _minLevel;
        private readonly ITelemetryClient _logger;

        public ApplicationInsightsLogger(IConfiguration configuration)
        {
            if (!Enum.TryParse(configuration.GetValue("Logging:LogLevel:Default", LogLevel.Debug.ToString()).ToUpper(), out _minLevel))
                throw new ArgumentException("Invalid log level");
        }

        public void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters)
        {
            if (_minLevel < severity)
                return;
        }

        public void LogCritical(string message, Exception exception = null, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.CRITICAL)
                throw new NotImplementedException();
        }

        public void LogDebug(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.DEBUG)
                throw new NotImplementedException();
        }

        public void LogError(string message, Exception exception = null, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.ERROR)
                throw new NotImplementedException();
        }

        public void LogInformation(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.INFORMATION)
                throw new NotImplementedException();
        }

        public void LogWarning(string message, params (string name, string value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.WARNING)
                throw new NotImplementedException();
        }

        public void LogWarning(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.WARNING)
                throw new NotImplementedException();
        }

        private SeverityLevel GetSeverityLevel(LoggerManagerSeverity severity)
        {
            return severity switch
            {
                LoggerManagerSeverity.INFORMATION => SeverityLevel.Information,
                LoggerManagerSeverity.WARNING => SeverityLevel.Warning,
                LoggerManagerSeverity.ERROR => SeverityLevel.Error,
                LoggerManagerSeverity.CRITICAL => SeverityLevel.Critical,
                _ => SeverityLevel.Verbose,
            };
        }
    }
}
