using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.Login;

public class LoginUserRequest : IRequest<LoginUserViewModel>
{
    public string Password { get; set; }
    
    public string Email { get; set; }
}