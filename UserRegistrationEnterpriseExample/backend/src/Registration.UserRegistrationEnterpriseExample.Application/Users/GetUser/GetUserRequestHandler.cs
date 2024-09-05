using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Application.Users.GetUser;

namespace Registration.UserRegistrationEnterpriseExample.Application.Usuarios.ObterUsuario;

public class GetUserRequestHandler : IRequestHandler<GetUserRequest, GetUserViewModel>
{
    private readonly IUsers _users;
    private readonly IUserWorkshops _userWorkshops;

    public GetUserRequestHandler(IUsers users, IUserWorkshops userWorkshops)
    {
        _users = users;
        _userWorkshops = userWorkshops;
    }

    public async Task<GetUserViewModel> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.SelecionarPorDocumentoOuEmail(request.Document, request.Email);

        if (user == null)
        {
            throw new RecursoNaoEncontradoException("O usuário não fui encontrado em nossa base de dados");
        }

        var userWorkshops = 
            await _userWorkshops.ObterVariasPorUserId(user.Id);

        return new GetUserViewModel
        {
            UserType = ((int)user.UserType).ToString(),
            Document = user.Document,
            Name = user.Name,
            WorkShopsSubscribed = userWorkshops.Any() ? userWorkshops.Select(x => new UserWorkshopModel
            {
                Id = x.WorkShop.Id.ToString(),
                Name = x.WorkShop.Name,
                Date = x.WorkShop.Address,
                Description = x.WorkShop.Description,
                Address = x.WorkShop.Address,
                ImageCreator = x.WorkShop.ImageCreator,
                Image = x.WorkShop.Image,
                IdCreator = x.WorkShop.IdCreator
            }).ToList() : new List<UserWorkshopModel>()
        };
    }
}