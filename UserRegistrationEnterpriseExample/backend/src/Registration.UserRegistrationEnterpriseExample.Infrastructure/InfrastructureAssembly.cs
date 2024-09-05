using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure;

public static class InfrastructureAssembly
{
    public static string Name => typeof(DatabaseContext).Assembly.GetName().Name;
}