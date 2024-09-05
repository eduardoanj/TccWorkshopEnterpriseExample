namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

public class ScopedDatabaseContext : IScopedDatabaseContext
{
    public ScopedDatabaseContext(DatabaseContext context)
    {
        Context = context;
    }

    public DatabaseContext Context { get; }
}