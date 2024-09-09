using System.Linq.Expressions;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Database;

public interface IBaseRepository<T> where T : EntidadeAuditavel
{
    Task InserirOuAtualizar(T entidade);
}