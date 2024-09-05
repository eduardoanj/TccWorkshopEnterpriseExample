using Microsoft.Extensions.Hosting;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Exceptions;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Extensions;
using Serilog;
using Serilog.Filters;
using Serilog.Templates;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Logging;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, string serviceName)
    {
        return builder.UseSerilog((context, configuration) =>
        {
            var loggerConfiguration = configuration.Enrich.FromLogContext()
                .Filter
                .ByExcluding(Matching.WithProperty<string>("RequestPath", s => s.Contains("healthcheck")));

            if (Environment.GetEnvironmentVariable("USE_JSON_LOG") == "false")
                loggerConfiguration.WriteTo.Console();
            else
                loggerConfiguration.WriteTo.Console(new ExpressionTemplate(
                    "{ {dd:{trace_id:@p['dd_trace_id'], span_id:@p['dd_span_id']}, date: @t, level: @l, message: @m, exception: @x, properties: @p} }\n"));
        });
    }

    internal static bool ShouldSendToSentry(Exception exception)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var ehAmbienteDev = environment == Environments.Development || environment?.ToUpper() == "LOCAL";

        exception = exception is RequeueException
            ? exception.InnerException
            : exception;
        return !exception.IsBusinessException() && !ehAmbienteDev;
    }
}