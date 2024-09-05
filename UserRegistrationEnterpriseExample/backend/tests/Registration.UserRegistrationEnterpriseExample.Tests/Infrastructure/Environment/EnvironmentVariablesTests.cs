using FluentAssertions;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.EnvironmentInformation;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Infrastructure.Environment;

public class EnvironmentVariablesTests
{
    [Fact]
    public void Should_return_environment_variable_value_as_it_is()
    {
        System.Environment.SetEnvironmentVariable("SPACEX", "IS AWESOME!");

        var environmentVariables = new EnvironmentVariables();
        var environmentVariableValue = environmentVariables.GetEnvironmentVariable("SPACEX");

        environmentVariableValue.Should().Be("IS AWESOME!");
    }
}