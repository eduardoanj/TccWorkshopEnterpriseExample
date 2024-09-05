using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;

public class CancellationTokenBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IRequestContext _requestContext;

    public CancellationTokenBehavior(IRequestContext requestContext)
    {
        _requestContext = requestContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _requestContext.CancellationToken
            .ThrowIfCancellationRequested();
        return await next();
    }
}