using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Extensions;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.AspNetCore.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async httpContext =>
            {
                var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    var logger = loggerFactory.CreateLogger(nameof(ExceptionHandlerExtension));

                    if (exceptionHandlerFeature.Error.IsBusinessException())
                        logger.LogWarning($"Unexpected error: {exceptionHandlerFeature.Error.Message}");
                    else
                        logger.LogError(exceptionHandlerFeature.Error, "Unexpected error");

                    var (httpStatusCode, message) = exceptionHandlerFeature.Error switch
                    {
                        RecursoNaoEncontradoException exception => (HttpStatusCode.NotFound, exception.Message),
                        var error => (HttpStatusCode.InternalServerError, BuildErrorMessage(error))
                    };

                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = (int) httpStatusCode;

                    var json = new
                    {
                        Detailed = (string) null,
                        httpContext.Response.StatusCode,
                        Message = message
                    };

                    await httpContext.Response.WriteAsJsonAsync(json);
                }
            });
        });
    }

    private static string BuildErrorMessage(Exception exception)
    {
        return System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
            ? exception.Message
            : "Unexpected erro while proccessing the request";
    }
}