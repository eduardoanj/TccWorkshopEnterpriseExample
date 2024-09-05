using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Registration.UserRegistrationEnterpriseExample.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey
            });
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UserRegistrationEnterpriseExample",
                Description = "API to...",
                Version = "v1"
            });
            options.ExampleFilters();

            var filePath = Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            options.IncludeXmlComments(filePath);
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }
}