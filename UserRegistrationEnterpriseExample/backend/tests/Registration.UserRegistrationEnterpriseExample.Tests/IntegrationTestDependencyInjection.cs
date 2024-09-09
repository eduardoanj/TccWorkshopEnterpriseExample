using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;


namespace Registration.UserRegistrationEnterpriseExample.Tests;

public static class IntegrationTestDependencyInjection
{
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