using MediatR;
using Microsoft.Extensions.Logging;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;

public class RequestPayloadLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    private readonly IRequestContext _requestContext;

    public RequestPayloadLoggingBehavior(ILogger<TRequest> logger, IRequestContext requestContext)
    {
        _logger = logger;
        _requestContext = requestContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        LogPayload(request);
        return await next();
    }

    private void LogPayload(TRequest request)
    {
        if (!_logger.IsEnabled(LogLevel.Debug))
            return;

        var requestName = typeof(TRequest).Name;
        var userId = _requestContext.CurrentUserId ?? string.Empty;
    }
}