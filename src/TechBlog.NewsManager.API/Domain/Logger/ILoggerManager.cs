namespace TechBlog.NewsManager.API.Domain.Logger
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogInformation(string message, params (string name, object value)[] parameters);
        void LogInformation(string message, params (string name, string value)[] parameters);
        void LogWarning(string message);
        void LogWarning(string message, params (string name, string value)[] parameters);
        void LogWarning(string message, params (string name, object value)[] parameters);
        void LogError(string message, Exception exception = default);
        void LogError(string message, Exception exception = default, params (string name, object value)[] parameters);
        void LogError(string message, Exception exception = default, params (string name, string value)[] parameters);
        void LogCritical(string message, Exception exception = default);
        void LogCritical(string message, Exception exception = default, params (string name, object value)[] parameters);
        void LogCritical(string message, Exception exception = default, params (string name, string value)[] parameters);
        void LogDebug(string message);
        void LogDebug(string message, params (string name, object value)[] parameters);
        void LogDebug(string message, params (string name, string value)[] parameters);
        void Log(string message, LoggerManagerSeverity severity, params (string name, string value)[] parameters);
        void Log(string message, LoggerManagerSeverity severity, params (string name, object value)[] parameters);
    }
}
