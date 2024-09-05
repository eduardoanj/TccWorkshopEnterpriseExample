using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

public class WorkShop : EntidadeAuditavel
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string ImageCreator { get; set; }
    public string IdCreator { get; set; }
    public ICollection<UserWorkshop> UsersSubscribed { get; set; } = new List<UserWorkshop>();
}
