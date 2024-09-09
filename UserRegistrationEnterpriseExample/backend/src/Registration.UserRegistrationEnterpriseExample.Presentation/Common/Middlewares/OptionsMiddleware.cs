namespace Registration.UserRegistrationEnterpriseExample.Presentation.Common.Middlewares;

public class OptionsMiddleware
{
    private readonly RequestDelegate _next;

    public OptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        return BeginInvoke(httpContext);
    }

    private Task BeginInvoke(HttpContext httpContext)
    {
        httpContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] {"Authorization", "Access-Control-Allow-Headers", "Content-Type", "Access-Control-Request-Method", "Access-Control-Request-Headers"});

        return _next.Invoke(httpContext);
    }
}