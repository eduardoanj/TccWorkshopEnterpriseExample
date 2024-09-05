using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.Vinculate;

public class VinculateWorkshopToUserRequestHandler : IRequestHandler<VinculateWorkshopToUserRequest, Unit>
{
    private readonly IUsers _users;
    private readonly IWorkshops _workshops;
    private readonly IUserWorkshops _userWorkshops;

    public VinculateWorkshopToUserRequestHandler(IWorkshops workshops, IUsers users, IUserWorkshops userWorkshops)
    {
        _workshops = workshops;
        _users = users;
        _userWorkshops = userWorkshops;
    }

    public async Task<Unit> Handle(VinculateWorkshopToUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.SelecionarPorId(request.UserId);

        if (user == null)
        {
            throw new RecursoNaoEncontradoException("Usuário não encontrado");
        }

        var workshop = await _workshops.SelecionarPorId(request.WorkshopId);
        
        if (workshop == null)
        {
            throw new RecursoNaoEncontradoException("Workshop não encontrado");
        }

        await _userWorkshops.Vincular(new UserWorkshop
        {
            UserId = user.Id,
            WorkshopId = workshop.Id
        });
        
        return Unit.Value;
    }
}