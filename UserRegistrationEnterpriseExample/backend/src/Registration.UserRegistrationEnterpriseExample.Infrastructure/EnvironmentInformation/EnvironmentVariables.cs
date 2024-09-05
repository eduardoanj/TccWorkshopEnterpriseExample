namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;

public class EnvironmentVariables : IEnvironmentVariables
{
    public string GetEnvironmentVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName);
    }
}