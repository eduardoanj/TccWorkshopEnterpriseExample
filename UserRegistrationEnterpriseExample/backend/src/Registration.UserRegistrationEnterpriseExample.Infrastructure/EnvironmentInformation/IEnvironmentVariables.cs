namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;

public interface IEnvironmentVariables
{
    string GetEnvironmentVariable(string variableName);
}