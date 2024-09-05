using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

public class UserWorkshop : EntidadeAuditavel
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid WorkshopId { get; set; }
    public WorkShop WorkShop { get; set; }
}