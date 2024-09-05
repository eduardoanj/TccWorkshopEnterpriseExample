using FluentAssertions;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.Extensions;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Infrastructure.Extensions;

public class ExceptionExtensionsTest
{
    [Theory]
    [MemberData(nameof(ExceptionsTestCase.Exceptions), MemberType = typeof(ExceptionsTestCase))]
    public void Deve_validar_se_a_exception_eh_de_negocio(Exception exception, bool validacao)
    {
        var ehExceptionNegocio = exception.IsBusinessException();
        ehExceptionNegocio.Should().Be(validacao);
    }
}