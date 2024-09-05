using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;

public class SaveUserRequest : IRequest<SaveUserViewModel>
{
    public string Document { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; }
}