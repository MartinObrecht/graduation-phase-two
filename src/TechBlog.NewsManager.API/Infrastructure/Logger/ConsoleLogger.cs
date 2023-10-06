using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using TechBlog.NewsManager.API.Domain.Logger;

namespace TechBlog.NewsManager.API.Infrastructure.Logger
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLogger : ILoggerManager
    {
        private readonly ILogger<ConsoleLogger> _logger;
        private readonly LoggerManagerSeverity _minLevel;

        public ConsoleLogger(ILogger<ConsoleLogger> logger, IConfiguration configuration)
        {
            _logger = logger;
            if (!Enum.TryParse(configuration.GetValue("Logging:LogLevel:Default", LogLevel.Debug.ToString()).ToUpper(), out _minLevel))
                throw new ArgumentException("Invalid log level");
        }

        public void LogDebug(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.DEBUG)
                Log(message, LoggerManagerSeverity.DEBUG, parameters);
        }

        public void LogDebug(string message, params (string name, string value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.DEBUG)
                Log(message, LoggerManagerSeverity.DEBUG, parameters);
        }

        public void LogInformation(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.INFORMATION)
                Log(message, LoggerManagerSeverity.INFORMATION, parameters);
        }

        public void LogInformation(string message, params (string name, string value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.INFORMATION)
                Log(message, LoggerManagerSeverity.INFORMATION, parameters);
        }

        public void LogWarning(string message, params (string name, string value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.WARNING)
                Log(message, LoggerManagerSeverity.WARNING, parameters);
        }

        public void LogWarning(string message, params (string name, object value)[] parameters)
        {
            if (_minLevel >= LoggerManagerSeverity.WARNING)
                Log(message, LoggerManagerSeverity.WARNING, parameters);
        }

        public void LogError(string message, Exception exception = default, params (string name, object value)[] parameters)
        {
            if (_minLevel < LoggerManagerSeverity.ERROR)
                return;

            var newParameters = new (string name, object value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), exception);

            Log(message, LoggerManagerSeverity.ERROR, newParameters);
        }

        public void LogError(string message, Exception exception = default, params (string name, string value)[] parameters)
        {
            if (_minLevel < LoggerManagerSeverity.ERROR)
                return;

            var newParameters = new (string name, string value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), JsonConvert.SerializeObject(exception));

            Log(message, LoggerManagerSeverity.ERROR, newParameters);
        }

        public void LogCritical(string message, Exception exception = default, params (string name, string value)[] parameters)
        {
            if (_minLevel < LoggerManagerSeverity.CRITICAL)
                return;

            var newParameters = new (string name, string value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), JsonConvert.SerializeObject(exception));

            Log(message, LoggerManagerSeverity.CRITICAL, newParameters);
        }

        public void LogCritical(string message, Exception exception = default, params (string name, object value)[] parameters)
        {
            if (_minLevel < LoggerManagerSeverity.CRITICAL)
                return;

            var newParameters = new (string name, object value)[parameters.Length + 1];

            parameters.CopyTo(newParameters, 0);

            if (exception != null)
                newParameters[^1] = (nameof(exception), exception);

            Log(message, LoggerManagerSeverity.CRITICAL, newParameters);
        }

        public void Log(string message, LoggerManagerSeverity severity, params (string name, object value)[] parameters)
        {
            parameters ??= Array.Empty<(string name, object value)>();
            var parametersAsString = new (string name, string value)[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
                parametersAsString[i] = (parameters[i].name, JsonConvert.SerializeObject(parameters[i].value));

            Log(message, severity, parametersAsString);
        }

        [SuppressMessage("Usage", "CA2254", Justification = "")]
        public void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters)
        {
            if (_minLevel < severity)
                return;

            switch (severity)
            {
                case LoggerManagerSeverity.INFORMATION:
                    _logger.LogInformation(message, parameters);
                    break;
                case LoggerManagerSeverity.WARNING:
                    _logger.LogWarning(message, parameters);
                    break;
                case LoggerManagerSeverity.ERROR:
                    _logger.LogError(message, parameters);
                    break;
                case LoggerManagerSeverity.CRITICAL:
                    _logger.LogCritical(message, parameters);
                    break;
                default:
                    _logger.LogDebug(message, parameters);
                    break;
            }
        }
    }
}
