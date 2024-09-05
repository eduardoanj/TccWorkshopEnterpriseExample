using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.Login;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginUserViewModel>
{
    private readonly IUsers _users;

    public LoginUserRequestHandler(IUsers users)
    {
        _users = users;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var selecionarPorDocumento = await _users.ObterUsuarioPorEmailEPassword(request.Email, request.Password);

        var visual = selecionarPorDocumento != null;

        return new LoginUserViewModel
        {
            Visualizar = visual,
            Email = selecionarPorDocumento?.Email ?? string.Empty,
            Id = selecionarPorDocumento?.Id.ToString() ?? string.Empty,
        };
    }
}