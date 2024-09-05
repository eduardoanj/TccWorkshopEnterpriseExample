using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Application;
using Registration.UserRegistrationEnterpriseExample.Domain;
using Registration.UserRegistrationEnterpriseExample.Infrastructure;
using Registration.UserRegistrationEnterpriseExample.Tests.TestHelpers;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Common;

public static class TestIntegrationServiceCollectionFactory
{
    public static IServiceCollection BuildIntegrationTestInfrastructure(string testDatabaseName,
        Func<IntegrationTestClock> getIntegrationTestClock)
    {
        var services = new ServiceCollection();
        services.AddIntegrationTestDatabase(testDatabaseName);
        services.AddIntegrationTestLogs();
        services.AddDomain();
        services.AddApplication();
        services.AddInfrastructure();
        services.AddIntegrationTestClock(getIntegrationTestClock);

        return services;
    }

    public static IntegrationTestClock BuildIntegrationTestClock()
    {
        return new IntegrationTestClock(2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))));
    }
}