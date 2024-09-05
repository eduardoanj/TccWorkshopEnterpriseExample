using Registration.UserRegistrationEnterpriseExample.Application.Common.Database;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IUsers : IBaseRepository<User>
{
    Task<User> SelecionarPorId(Guid id);
    Task<User> ObterUsuarioPorEmailEPassword(string email, string senha);
    Task<User> SelecionarPorDocumentoOuEmail(string documento, string email);
}