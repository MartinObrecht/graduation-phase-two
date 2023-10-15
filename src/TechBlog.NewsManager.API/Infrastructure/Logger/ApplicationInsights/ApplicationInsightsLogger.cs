using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using TechBlog.NewsManager.API.Domain.Logger;

namespace TechBlog.NewsManager.API.Infrastructure.Logger.ApplicationInsights
{
    [ExcludeFromCodeCoverage]
    public class ApplicationInsightsLogger : ILoggerManager
    {
        private readonly LoggerManagerSeverity _minLevel;
        private readonly TelemetryClient _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationInsightsLogger(IConfiguration configuration, TelemetryClient logger, IHttpContextAccessor contextAccessor)
        {
            if (!Enum.TryParse(configuration.GetValue("Logging:LogLevel:Default", LogLevel.Debug.ToString()).ToUpper(), out _minLevel))
                throw new ArgumentException("Invalid log level");

            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public void LogDebug(string message) =>
            LogDebug(message, Array.Empty<(string name, string value)>());

        public void LogDebug(string message, params (string name, string value)[] parameters) =>
            Log(message, LoggerManagerSeverity.DEBUG, parameters);

        public void LogDebug(string message, params (string name, object value)[] parameters) =>
            Log(message, LoggerManagerSeverity.DEBUG, parameters);

        public void LogInformation(string message) =>
           LogInformation(message, Array.Empty<(string name, string value)>());

        public void LogInformation(string message, params (string name, string value)[] parameters) =>
            Log(message, LoggerManagerSeverity.INFORMATION, parameters);

        public void LogInformation(string message, params (string name, object value)[] parameters) =>
            Log(message, LoggerManagerSeverity.INFORMATION, parameters);

        public void LogWarning(string message) =>
           LogWarning(message, Array.Empty<(string name, string value)>());

        public void LogWarning(string message, params (string name, string value)[] parameters) =>
            Log(message, LoggerManagerSeverity.WARNING, parameters);

        public void LogWarning(string message, params (string name, object value)[] parameters) =>
            Log(message, LoggerManagerSeverity.WARNING, parameters);

        public void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters)
        {
            if (_minLevel > severity)
                return;

            var traceTelemetry = new TraceTelemetry
            {
                Message = message,
                SeverityLevel = GetSeverityLevel(severity),
            };

            for (int i = 0; i < parameters.Length; i++)
                traceTelemetry.Properties.Add(parameters[i].name, parameters[i].value);

            var currentRequest = _contextAccessor.HttpContext.Features.Get<RequestTelemetry>();

            if (currentRequest != null)
            {
                traceTelemetry.Context.Operation.Id = currentRequest.Context.Operation.Id;
                traceTelemetry.Context.Operation.ParentId = currentRequest.Context.Operation.ParentId;
            }

            _logger.TrackTrace(traceTelemetry);
        }

        public void Log(string message, LoggerManagerSeverity severity, params (string name, object value)[] parameters)
        {
            if (_minLevel >= severity)
                return;

            var traceTelemetry = new TraceTelemetry
            {
                Message = message,
                SeverityLevel = GetSeverityLevel(severity),
            };

            for (int i = 0; i < parameters.Length; i++)
                traceTelemetry.Properties.Add(parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            var currentRequest = _contextAccessor.HttpContext.Features.Get<RequestTelemetry>();

            if (currentRequest != null)
            {
                traceTelemetry.Context.Operation.Id = currentRequest.Context.Operation.Id;
                traceTelemetry.Context.Operation.ParentId = currentRequest.Context.Operation.ParentId;
            }

            _logger.TrackTrace(traceTelemetry);
        }

        public void LogError(string message, Exception exception = null) =>
            LogError(message, exception, Array.Empty<(string name, string value)>());

        public void LogError(string message, Exception exception = default, params (string name, string value)[] parameters) =>
            LogException(message, LoggerManagerSeverity.ERROR, exception, parameters);

        public void LogError(string message, Exception exception = default, params (string name, object value)[] parameters) =>
            LogException(message, LoggerManagerSeverity.ERROR, exception, parameters);

        public void LogCritical(string message, Exception exception = null) =>
            LogCritical(message, exception, Array.Empty<(string name, string value)>());

        public void LogCritical(string message, Exception exception = default, params (string name, string value)[] parameters) =>
            LogException(message, LoggerManagerSeverity.CRITICAL, exception, parameters);

        public void LogCritical(string message, Exception exception = default, params (string name, object value)[] parameters) =>
            LogException(message, LoggerManagerSeverity.CRITICAL, exception, parameters);

        private void LogException(string message, LoggerManagerSeverity severity, Exception exception, params (string name, string value)[] parameters)
        {
            if (_minLevel >= severity)
                return;

            Log(message, severity, parameters);

            if (exception == null)
                return;

            var exceptionTelemetry = new ExceptionTelemetry
            {
                Message = exception.Message,
                Exception = exception,
            };

            for (int i = 0; i < parameters.Length; i++)
                exceptionTelemetry.Properties.Add(parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            var currentRequest = _contextAccessor.HttpContext.Features.Get<RequestTelemetry>();

            if (currentRequest != null)
            {
                exceptionTelemetry.Context.Operation.Id = currentRequest.Context.Operation.Id;
                exceptionTelemetry.Context.Operation.ParentId = currentRequest.Context.Operation.ParentId;
            }

            _logger.TrackException(exceptionTelemetry);
        }

        private void LogException(string message, LoggerManagerSeverity severity, Exception exception, params (string name, object value)[] parameters)
        {
            if (_minLevel > severity)
                return;

            Log(message, severity, parameters);

            if (exception == null)
                return;

            var exceptionTelemetry = new ExceptionTelemetry
            {
                Message = exception.Message,
                Exception = exception,
            };

            for (int i = 0; i < parameters.Length; i++)
                exceptionTelemetry.Properties.Add(parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            var currentRequest = _contextAccessor.HttpContext.Features.Get<RequestTelemetry>();

            if (currentRequest != null)
            {
                exceptionTelemetry.Context.Operation.Id = currentRequest.Context.Operation.Id;
                exceptionTelemetry.Context.Operation.ParentId = currentRequest.Context.Operation.ParentId;
            }

            _logger.TrackException(exceptionTelemetry);
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

        private static Dictionary<string, string> AsDictionary((string name, object value)[] parameters)
        {
            var response = new Dictionary<string, string>(parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                response.Add(parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            return response;
        }

        private static Dictionary<string, string> AsDictionary((string name, string value)[] parameters)
        {
            var response = new Dictionary<string, string>(parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                response.Add(parameters[i].name, parameters[i].value);

            return response;
        }
    }
}
