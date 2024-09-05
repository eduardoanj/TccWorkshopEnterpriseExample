using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.DateAndTime;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Identity;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Repositories;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentVariables, EnvironmentVariables>();

        services.AddScoped<RequestContext>();
        services.AddScoped<IRequestContext>(serviceProvider => serviceProvider.GetService<RequestContext>());

        services.AddSingleton<IClock, SystemClock>();

        services.AddScoped<IUsers, UsersRepository>();
        services.AddScoped<IWorkshops, WorkshopsRepository>();
        services.AddScoped<IUserWorkshops, UserWorkshopsRepository>();

        AddPostgreSql(services);
    }

    private static void AddPostgreSql(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

        var serviceProvider = services.BuildServiceProvider();
        var connectionString = serviceProvider
            .GetRequiredService<IConnectionStringFactory>()
            .GetConnectionString();

        services
            .AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    builder => { builder.MigrationsAssembly(InfrastructureAssembly.Name); });
            });

        services.AddHealthChecks()
            .AddDbContextCheck<DatabaseContext>()
            .AddNpgSql(connectionString);

        services.AddScoped<IScopedDatabaseContext, ScopedDatabaseContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}