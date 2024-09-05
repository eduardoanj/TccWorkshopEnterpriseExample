using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Tests.Common;
using Registration.UserRegistrationEnterpriseExample.Tests.TestHelpers;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application;

public abstract class IntegrationTestBase
{
    private const string DatabaseName = "UserRegistrationEnterpriseExample_IntegrationTests";

    private readonly Lazy<IServiceProvider> _serviceProvider;

    static IntegrationTestBase()
    {
        var serviceCollection = TestIntegrationServiceCollectionFactory.BuildIntegrationTestInfrastructure(
            DatabaseName,
            () => IntegrationTestClock
        );

        TestIntegrationDatabaseManager.RebuildIntegrationDatabase(GetServiceProvider(serviceCollection));
    }

    protected IntegrationTestBase()
    {
        var serviceCollection = TestIntegrationServiceCollectionFactory.BuildIntegrationTestInfrastructure(
            DatabaseName,
            () => IntegrationTestClock
        );
        _serviceProvider = new Lazy<IServiceProvider>(() => GetServiceProvider(serviceCollection));

        TestIntegrationDatabaseManager.TruncateAllDatabaseTables(GetServiceProvider(serviceCollection));
        IntegrationTestClock = TestIntegrationServiceCollectionFactory.BuildIntegrationTestClock();
    }
    
    protected static IntegrationTestClock IntegrationTestClock { get; private set; }

    public static IServiceProvider GetServiceProvider(IServiceCollection serviceCollection)
    {
        var defaultServiceProviderFactory = new DefaultServiceProviderFactory(new ServiceProviderOptions());
        return defaultServiceProviderFactory.CreateServiceProvider(serviceCollection);
    }
    
    [DebuggerStepThrough]
    protected Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest<TResponse>
    {
        var scope = _serviceProvider.Value.CreateScope();
        var mediator = scope.ServiceProvider.GetService<IMediator>();
        return mediator!.Send(request, CancellationToken.None);
    }
    
    protected DbSet<TEntity> GetTable<TEntity>()
        where TEntity : class
    {
        var scope = _serviceProvider.Value.CreateScope();
        var databaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
        return databaseContext!.Set<TEntity>();
    }

    protected async Task<TEntity> GetByIdAsync<TEntity>(Guid id)
        where TEntity : class
    {
        var scope = _serviceProvider.Value.CreateScope();
        var databaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
        return await databaseContext!.FindAsync<TEntity>(id);
    }
}