using System.Globalization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Registration.UserRegistrationEnterpriseExample.Application;
using Registration.UserRegistrationEnterpriseExample.Domain;
using Registration.UserRegistrationEnterpriseExample.Infrastructure;
using Registration.UserRegistrationEnterpriseExample.Presentation;
using Registration.UserRegistrationEnterpriseExample.Presentation.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();

startup.ConfigureServices(builder.Services);

var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

startup.Configure(app, loggerFactory);
app.Run();

namespace Registration.UserRegistrationEnterpriseExample.Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplication();
            services.AddDomain();
            services.AddInfrastructure();
            services.AddPresentation();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) 
                .AllowCredentials());

            app.UseMiddleware<OptionsMiddleware>();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck/liveness", new HealthCheckOptions {Predicate = _ => false});
                endpoints.MapHealthChecks("/healthcheck/readiness");
            });

            ConfigureCulture();
        }

        private static void ConfigureCulture()
        {
            var ptBrCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = ptBrCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ptBrCulture;
        }
    }
}