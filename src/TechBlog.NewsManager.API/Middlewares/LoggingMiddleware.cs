namespace TechBlog.NewsManager.API.Middlewares
{
    public sealed class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly (bool LogRequestHeaders, 
                          bool LogRequestBody,
                          bool LogResponseHeaders,
                          bool LogResponseBody) _loggingConfiguration;

        public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;

            _loggingConfiguration = (configuration.GetValue("Logging:Configuration:LogRequestHeaders", false),
                                     configuration.GetValue("Logging:Configuration:LogRequestBody", false),
                                     configuration.GetValue("Logging:Configuration:LogResponseHeaders", false),
                                     configuration.GetValue("Logging:Configuration:LogResponseBody", false));
        }

        public async Task InvokeAsync(HttpContext context)
        {

        }

        private void LogRequestHeadersIfNeended()
        {
            if (!_loggingConfiguration.LogRequestHeaders)
                return;

            throw new NotImplementedException();
        }

        private void LogRequestBodyIfNeended()
        {
            if (!_loggingConfiguration.LogRequestBody)
                return;

            throw new NotImplementedException();
        }

        private void LogResponseHeadersIfNeended()
        {
            if (!_loggingConfiguration.LogResponseHeaders)
                return;

            throw new NotImplementedException();
        }

        private void LogResponseBodyIfNeended()
        {
            if (!_loggingConfiguration.LogResponseBody)
                return;

            throw new NotImplementedException();
        }
    }
}
