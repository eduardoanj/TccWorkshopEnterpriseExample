using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Persistir;

public class SaveWorkshopRequestHandler : IRequestHandler<SaveWorkshopRequest, Guid>
{
    private readonly IWorkshops _workshops;

    public SaveWorkshopRequestHandler(IWorkshops workshops)
    {
        _workshops = workshops;
    }

    public async Task<Guid> Handle(SaveWorkshopRequest request, CancellationToken cancellationToken)
    {
        var workshop = new WorkShop
        {
            Name = request.Name,
            Description = request.Description,
            Date = DateTime.Parse(request.Date),
            Address = request.Address,
            Image = request.Image,
            ImageCreator = string.Empty,
            IdCreator = request.IdCreator
        };

        await _workshops.InserirOuAtualizar(workshop);
        return workshop.Id;
    }
}