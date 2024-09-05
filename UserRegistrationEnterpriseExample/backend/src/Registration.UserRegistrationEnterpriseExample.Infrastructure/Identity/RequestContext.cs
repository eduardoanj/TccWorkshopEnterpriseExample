using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Identity;

public class RequestContext : IRequestContext
{
    public virtual string CurrentUserId { get; set; } =
        "user";
    public virtual CancellationToken CancellationToken { get; set; }
}