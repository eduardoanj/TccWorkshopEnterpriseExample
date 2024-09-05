using Npgsql;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public class ConnectionStringFactory : IConnectionStringFactory
{
    private readonly IEnvironmentVariables _environmentVariables;

    public ConnectionStringFactory(IEnvironmentVariables environmentVariables)
    {
        _environmentVariables = environmentVariables;
    }

    private string Host => _environmentVariables.GetEnvironmentVariable("POSTGRESQL_HOST") ?? "127.0.0.1";
    private int Port => int.Parse(_environmentVariables.GetEnvironmentVariable("POSTGRESQL_PORT") ?? "5432");

    private string Database => _environmentVariables.GetEnvironmentVariable("POSTGRESQL_DATABASE") ??
                               "templatecleanarchitecture";

    private string Username => _environmentVariables.GetEnvironmentVariable("POSTGRESQL_USERNAME") ?? "postgres";
    private string Password => _environmentVariables.GetEnvironmentVariable("POSTGRESQL_PASSWORD") ?? "admin";
    private string SSlMode => _environmentVariables.GetEnvironmentVariable("POSTGRESQL_SSL_MODE") ?? "Disable";

    private int MinimumPoolSize =>
        int.Parse(_environmentVariables.GetEnvironmentVariable("POSTGRESQL_MINIMUM_POOL_SIZE") ?? "10");

    private int MaximumPoolSize =>
        int.Parse(_environmentVariables.GetEnvironmentVariable("POSTGRESQL_MAXIMUM_POOL_SIZE") ?? "20");

    private int CommandTimeout =>
        int.Parse(_environmentVariables.GetEnvironmentVariable("POSTGRESQL_COMMAND_TIMEOUT") ?? "60");

    public string GetConnectionString()
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = Username,
            Password = Password,
            SslMode = Enum.Parse<SslMode>(SSlMode),
            MinPoolSize = MinimumPoolSize,
            MaxPoolSize = MaximumPoolSize,
            CommandTimeout = CommandTimeout,
            Pooling = true
        }.ToString();
    }
}