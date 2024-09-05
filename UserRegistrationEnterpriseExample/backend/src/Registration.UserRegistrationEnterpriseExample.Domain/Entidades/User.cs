using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Domain.Enums;

namespace Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

public class User : EntidadeAuditavel
{
    public UserType UserType { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public ICollection<UserWorkshop> WorkShopsSubscribed { get; set; } = new List<UserWorkshop>();
}