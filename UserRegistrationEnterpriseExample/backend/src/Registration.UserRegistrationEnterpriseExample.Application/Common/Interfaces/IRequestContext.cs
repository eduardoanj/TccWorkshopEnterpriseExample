namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IRequestContext
{
    string CurrentUserId { get; }
    CancellationToken CancellationToken { get; }
}