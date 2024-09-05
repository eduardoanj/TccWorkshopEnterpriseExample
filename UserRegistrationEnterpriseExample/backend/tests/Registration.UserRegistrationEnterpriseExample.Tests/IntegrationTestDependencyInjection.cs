using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Presentation;
using Registration.UserRegistrationEnterpriseExample.Tests.TestHelpers;


namespace Registration.UserRegistrationEnterpriseExample.Tests;

public static class IntegrationTestDependencyInjection
{
    public static void AddIntegrationTestClock(this IServiceCollection services, Func<IntegrationTestClock> getClock)
    {
        services.AddScoped<IClock>(serviceProvider => getClock());
    }

    public static void AddIntegrationTestLogs(this IServiceCollection services)
    {
        services.AddSingleton(Substitute.For<ILoggerFactory>());
        foreach (var loggerType in GetLoggers().Value)
            services.AddSingleton(loggerType,
                Substitute.For(new[] {loggerType}, Array.Empty<object>()));
    }

    private static Lazy<IEnumerable<Type>> GetLoggers()
    {
        return new Lazy<IEnumerable<Type>>(() =>
        {
            var assemblies = new[]
            {
                typeof(EntidadeAuditavel).Assembly,
                typeof(UnitOfWorkBehavior<,>).Assembly,
                typeof(BaseRepository<>).Assembly,
                typeof(Startup).Assembly
            };
            var loggerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract)
                .SelectMany(x => x.GetConstructors())
                .SelectMany(x => x.GetParameters())
                .Select(x => x.ParameterType)
                .Where(x => x.IsGenericType && typeof(ILogger).IsAssignableFrom(x))
                .Where(x => !x.ContainsGenericParameters)
                .ToArray();

            var requestLoggerTypes = typeof(UnitOfWorkBehavior<,>).Assembly
                .GetTypes()
                .Where(x => typeof(IBaseRequest).IsAssignableFrom(x))
                .Select(x => typeof(ILogger<>).MakeGenericType(x))
                .ToArray();

            return loggerTypes.Union(requestLoggerTypes);
        });
    }

    public static void AddIntegrationTestDatabase(this IServiceCollection services, string databaseName)
    {
        var environmentVariables = Substitute.For<IEnvironmentVariables>();
        environmentVariables.GetEnvironmentVariable(Arg.Any<string>()).Returns((string) null);
        var config = new[]
        {
            ("POSTGRESQL_HOST", "127.0.0.1"),
            ("POSTGRESQL_PORT", "5432"),
            ("POSTGRESQL_DATABASE", databaseName),
            ("POSTGRESQL_USERNAME", "postgres"),
            ("POSTGRESQL_PASSWORD", "admin"),
            ("POSTGRESQL_SSL_MODE", "Disable")
        };
        foreach (var (variable, value) in config) environmentVariables.GetEnvironmentVariable(variable).Returns(value);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton(environmentVariables);
        services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

        var connectionString = services.BuildServiceProvider()
            .GetRequiredService<IConnectionStringFactory>()
            .GetConnectionString();

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString,
                builder => { builder.MigrationsAssembly(InfrastructureAssembly.Name); });
        });
    }
}