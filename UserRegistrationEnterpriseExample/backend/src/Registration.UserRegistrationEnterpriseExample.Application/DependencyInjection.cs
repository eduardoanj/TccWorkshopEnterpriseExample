using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Registration.UserRegistrationEnterpriseExample.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddFluentValidation(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public static void AddFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        var type = typeof(IValidator<>);

        var exportedTypes = assembly.GetExportedTypes()
            .Where(t =>
                t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))
            .ToList();

        foreach (var exportedType in exportedTypes)
        {
            var interfaceType = exportedType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
                .Select(i => i.GetGenericArguments()[0])
                .First();

            var genericType = type.MakeGenericType(interfaceType);

            services.AddTransient(genericType, exportedType);
        }
    }
}