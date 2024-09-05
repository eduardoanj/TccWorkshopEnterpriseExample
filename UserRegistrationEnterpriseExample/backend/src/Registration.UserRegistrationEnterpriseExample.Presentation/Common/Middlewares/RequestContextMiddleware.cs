using System.IdentityModel.Tokens.Jwt;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Identity;

namespace Registration.UserRegistrationEnterpriseExample.Presentation.Common.Middlewares;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IRequestContext requestContext)
    {
        var requestPath = httpContext.Request.Path;
        if (requestPath.StartsWithSegments("/api"))
        {
            if (requestContext is RequestContext r)
            {
                r.CurrentUserId = string.Empty;
            }
        }

        await _next(httpContext);
    }
}