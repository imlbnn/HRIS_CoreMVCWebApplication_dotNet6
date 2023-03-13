namespace HRISBlazorServerApp.Handlers
{
    public class NoOpDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<NoOpDelegatingHandler> _logger;

        public NoOpDelegatingHandler(ILogger<NoOpDelegatingHandler> logger) => _logger = logger;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.TryGetValues("X-Correlation-Id", out var headerEnumerable))
            {
                _logger.LogInformation("Request has the following correlation ID header {CorrelationId}.", headerEnumerable.FirstOrDefault());
            }
            else
            {
                _logger.LogInformation("Request does not have a correlation ID header.");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
