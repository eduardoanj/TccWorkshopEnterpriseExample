using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Logging;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Infrastructure.Logging;

public class LoggingConfigTests
{
    public static IEnumerable<object[]> AllSentryEnvironments
    {
        get
        {
            return new[]
            {
                new[] {Environments.Production},
                new[] {Environments.Staging}
            };
        }
    }

    public static IEnumerable<object[]> AllSentryExceptions
    {
        get
        {
            return new[]
            {
                new object[] {new Exception()},
                new object[] {new RecursoNaoEncontradoException("exception de negocio")}
            };
        }
    }

    [Theory]
    [MemberData(nameof(AllSentryEnvironments))]
    public void Deve_enviar_exception_para_sentry(string sentryEnvironment)
    {
        var exception = new Exception();
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", sentryEnvironment);

        var shouldSendToSentry = HostBuilderExtensions.ShouldSendToSentry(exception);

        shouldSendToSentry.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(AllSentryEnvironments))]
    public void Nao_deve_enviar_expection_para_sentry_por_business_exception(string sentryEnvironment)
    {
        var exception = new RecursoNaoEncontradoException("exception de negocio");
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", sentryEnvironment);

        var shouldSendToSentry = HostBuilderExtensions.ShouldSendToSentry(exception);

        shouldSendToSentry.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(AllSentryExceptions))]
    public void Nao_deve_enviar_expection_para_sentry_ambiente_local_ou_development(Exception sentryExceptions)
    {
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "local");

        var shouldSendToSentry = HostBuilderExtensions.ShouldSendToSentry(sentryExceptions);

        shouldSendToSentry.Should().BeFalse();
    }
}