using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Persistir;

public class SaveWorkshopRequest : IRequest<Guid>
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string IdCreator { get; set; }
}