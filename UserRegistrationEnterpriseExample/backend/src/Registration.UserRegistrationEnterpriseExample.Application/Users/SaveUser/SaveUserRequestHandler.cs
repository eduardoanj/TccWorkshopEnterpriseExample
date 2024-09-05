using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Domain.Enums;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;

internal class SaveUserRequestHandler : IRequestHandler<SaveUserRequest, SaveUserViewModel>
{
    private readonly IUsers _users;
    private readonly IClock _dateTime;

    public SaveUserRequestHandler(IUsers users, IClock dateTime)
    {
        _users = users;
        _dateTime = dateTime;
    }

    public async Task<SaveUserViewModel> Handle(SaveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _users.SelecionarPorDocumentoOuEmail(request.Document, request.Email);

        var viewModel = new SaveUserViewModel();
        viewModel.Email = request.Email;

        if (user == null)
        {
            user = new User();
            user.OriginTimestampUtc = _dateTime.Now;
            user.Document = request.Document;
            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            user.UserType = Enum.Parse<UserType>(request.UserType);

            await _users.InserirOuAtualizar(user);
            
            viewModel.UsuarioJaCadastrado = false;
            viewModel.Id = user.Id;
            return viewModel;
        }
        viewModel.UsuarioJaCadastrado = true;
        viewModel.Id = Guid.Empty;
        return viewModel;
    }
}