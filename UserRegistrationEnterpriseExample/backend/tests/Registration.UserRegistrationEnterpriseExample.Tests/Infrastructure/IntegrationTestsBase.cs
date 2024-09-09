using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;
using Registration.UserRegistrationEnterpriseExample.Tests.Common;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Infrastructure;

public abstract class IntegrationTestsBase : IDisposable
{
    private const string DatabaseName = "templatecleanarchitecture_IntegrationTests";
    private readonly Lazy<DatabaseContext> _databaseContext;

    private readonly Lazy<IServiceProvider> _rootServiceProvider;

    private readonly IServiceCollection _serviceCollection;
    private readonly Lazy<IServiceScope> _serviceScope;

    static IntegrationTestsBase()
    {
        var serviceCollection = TestIntegrationServiceCollectionFactory.BuildIntegrationTestInfrastructure(
            DatabaseName
        );

        TestIntegrationDatabaseManager.RebuildIntegrationDatabase(GetServiceProvider(serviceCollection));
    }

    protected IntegrationTestsBase()
    {
        _serviceCollection = TestIntegrationServiceCollectionFactory.BuildIntegrationTestInfrastructure(
            DatabaseName
        );

        TestIntegrationDatabaseManager.TruncateAllDatabaseTables(GetServiceProvider(_serviceCollection));
        _rootServiceProvider = new Lazy<IServiceProvider>(() => GetServiceProvider(_serviceCollection));
        _serviceScope = new Lazy<IServiceScope>(() => _rootServiceProvider.Value.CreateScope());
        _databaseContext = new Lazy<DatabaseContext>(() => ServiceProvider.GetService<DatabaseContext>());
    }
    
    protected IServiceScope ServiceScope => _serviceScope.Value;
    protected IServiceProvider ServiceProvider => ServiceScope.ServiceProvider;
    protected DatabaseContext DatabaseContext => _databaseContext.Value;
    protected IServiceProvider RootServiceProvider => _rootServiceProvider.Value;

    public void Dispose()
    {
        if (_serviceScope.IsValueCreated) _serviceScope.Value.Dispose();
    }

    public static IServiceProvider GetServiceProvider(IServiceCollection serviceCollection)
    {
        var defaultServiceProviderFactory = new DefaultServiceProviderFactory(new ServiceProviderOptions());
        return defaultServiceProviderFactory.CreateServiceProvider(serviceCollection);
    }

    protected (DatabaseContext, IUnitOfWork) GetNewUnitOfWork()
    {
        var scopedDatabaseContext = ServiceProvider.GetService<IScopedDatabaseContext>();
        var unitOfWork = new UnitOfWork(scopedDatabaseContext);

        return (scopedDatabaseContext!.Context, unitOfWork);
    }

    protected IScopedDatabaseContext GetScopedDatabaseContext()
    {
        var scopedDatabaseContext = ServiceProvider.GetService<IScopedDatabaseContext>();

        return scopedDatabaseContext;
    }
    
    protected T Mock<T>() where T : class
    {
        var mock = Substitute.For<T>();
        _serviceCollection.AddTransient(_ => mock);
        return mock;
    }

    protected T GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }

    #region Database Helpers

    protected IList<TEntity> GetEntities<TEntity>()
        where TEntity : class
    {
        var databaseContext = ServiceProvider.GetService<DatabaseContext>();
        return databaseContext!.Set<TEntity>().AsQueryable().ToList();
    }

    protected void InsertOne<TEntity>(TEntity entity)
        where TEntity : class
    {
        var databaseContext = ServiceProvider.GetService<DatabaseContext>();
        databaseContext!.Set<TEntity>().Add(entity);
        databaseContext.SaveChanges();
    }

    #endregion
}