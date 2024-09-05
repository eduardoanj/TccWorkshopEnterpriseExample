using Registration.UserRegistrationEnterpriseExample.Application.Common.Database;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IUserWorkshops : IBaseRepository<UserWorkshop>
{
    Task Vincular(UserWorkshop userWorkshop);
    Task<List<UserWorkshop>> ObterVariasPorUserId(Guid userId);
}