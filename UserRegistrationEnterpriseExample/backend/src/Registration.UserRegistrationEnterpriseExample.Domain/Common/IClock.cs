namespace Registration.UserRegistrationEnterpriseExample.Domain.Common;

public interface IClock
{
    DateTime Now { get; }
}