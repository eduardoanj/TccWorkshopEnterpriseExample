using System.Linq.Expressions;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Database;

public interface IBaseRepository<T> where T : EntidadeAuditavel
{
    Task Inserir(T entidade);
    Task Atualizar(T entidade);
    Task InserirOuAtualizar(T entidade);
    Task ExcluirFisicamente(T entidade);
    Task ExcluirLogicamente(T entidade);
    Task<IList<T>> SelecionarVariasPor(Expression<Func<T, bool>> filtro = null);
    Task<T> SelecionarUmaPor(Expression<Func<T, bool>> filtro = null);
    Task<T> ObterPorId(Guid id);
}