namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public interface IConnectionStringFactory
{
    string GetConnectionString();
}