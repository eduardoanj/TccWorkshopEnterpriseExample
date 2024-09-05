using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks.Actions;

namespace Registration.UserRegistrationEnterpriseExample.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddFluentValidation(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CancellationTokenBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPayloadLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DispatchEventsBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        services.AddScoped<IRollbackAction, ClearEventsToPublish>();
        services.AddScoped<IRollbackActionsExecuter, RollbackActionsExecuter>();
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