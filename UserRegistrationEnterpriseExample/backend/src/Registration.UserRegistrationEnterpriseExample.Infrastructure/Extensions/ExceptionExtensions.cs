using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Extensions;

public static class ExceptionExtensions
{
    public static bool IsBusinessException(this Exception exception)
    {
        return exception is INegocioException;
    }
}