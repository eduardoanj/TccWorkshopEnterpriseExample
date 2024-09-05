using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;

public class DispatchEventsBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();
        return response;
    }
}