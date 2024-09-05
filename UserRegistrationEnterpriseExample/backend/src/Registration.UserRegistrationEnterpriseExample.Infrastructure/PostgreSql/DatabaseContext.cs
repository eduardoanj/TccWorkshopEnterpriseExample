using Microsoft.EntityFrameworkCore;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Mappings;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<WorkShop> WorkShops { get; set; }
    public DbSet<UserWorkshop> UserWorkshops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var mappingTypes = typeof(UserMapping).Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IBaseMapping)))
            .Where(x => x.IsAbstract is false)
            .ToList();

        foreach (var mappingType in mappingTypes)
        {
            var mapping = Activator.CreateInstance(mappingType);

            var initializeMethod = mapping.GetType().GetMethod(nameof(IBaseMapping.MapearEntidade));

            initializeMethod.Invoke(mapping, new object[] {modelBuilder});
        }
    }
}