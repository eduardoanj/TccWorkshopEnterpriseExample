using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.GetUser;

public class GetUserRequest : IRequest<GetUserViewModel>
{
    public string Document { get; set; }
    public string Email { get; set; }
}