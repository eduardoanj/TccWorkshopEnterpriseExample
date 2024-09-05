using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Mappings;

namespace Registration.UserRegistrationEnterpriseExample.Tests;

public static class TestIntegrationDatabaseManager
{
    public static void RebuildIntegrationDatabase(IServiceProvider serviceProvider)
    {
        var databaseContext = serviceProvider.GetService<DatabaseContext>();
        databaseContext!.Database.EnsureDeleted();
        databaseContext.Database.Migrate();
    }

    public static void TruncateAllDatabaseTables(IServiceProvider serviceProvider)
    {
        var databaseContext = serviceProvider.GetService<DatabaseContext>();

        var tableNames = GetTableNames();
        foreach (var tableName in tableNames)
            databaseContext!.Database.ExecuteSqlRaw($"TRUNCATE TABLE {tableName} CASCADE");
    }

    private static IEnumerable GetTableNames()
    {
        var tableNames = typeof(UserMapping).Assembly
            .GetTypes()
            .Where(x => typeof(IBaseMapping).IsAssignableFrom(x))
            .Where(x => x.IsAbstract is false)
            .Select(x => (IBaseMapping) Activator.CreateInstance(x))
            .Select(x => x!.TableName)
            .ToList();
        return tableNames;
    }
}
