using System.Linq.Expressions;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Database;
using Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IWorkshops : IBaseRepository<WorkShop>
{
    Task<WorkShop> SelecionarPorId(Guid id);

    Task<(IList<WorkShop>, int)> ObterWorkshopsFiltrados(GetWorkshopRequest request);
}