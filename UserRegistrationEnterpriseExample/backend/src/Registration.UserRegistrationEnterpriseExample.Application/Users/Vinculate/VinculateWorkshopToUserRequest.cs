using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.Vinculate;

public class VinculateWorkshopToUserRequest : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public Guid WorkshopId { get; set; }
}