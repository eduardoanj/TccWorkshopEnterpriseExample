using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Infrastructure.Extensions;

public class ExceptionsTestCase
{
    public static IEnumerable<object[]> Exceptions
    {
        get
        {
            yield return new object[] {new RecursoNaoEncontradoException(string.Empty), true};
            yield return new object[] {new NullReferenceException(), false};
        }
    }
}