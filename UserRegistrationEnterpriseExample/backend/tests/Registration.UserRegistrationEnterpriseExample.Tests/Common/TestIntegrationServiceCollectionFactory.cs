using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Application;
using Registration.UserRegistrationEnterpriseExample.Domain;
using Registration.UserRegistrationEnterpriseExample.Infrastructure;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Common;

public static class TestIntegrationServiceCollectionFactory
{
    public static IServiceCollection BuildIntegrationTestInfrastructure(string testDatabaseName)
    {
        var services = new ServiceCollection();
        services.AddIntegrationTestDatabase(testDatabaseName);
        services.AddDomain();
        services.AddApplication();
        services.AddInfrastructure();

        return services;
    }
}