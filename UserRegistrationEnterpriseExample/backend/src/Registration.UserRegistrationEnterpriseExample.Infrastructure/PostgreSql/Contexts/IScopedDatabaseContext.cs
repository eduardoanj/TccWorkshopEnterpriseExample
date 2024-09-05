namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

public interface IScopedDatabaseContext
{
    DatabaseContext Context { get; }
}