using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Repositories;

public class UsersRepository : BaseRepository<User>, IUsers
{
    public UsersRepository(IScopedDatabaseContext scopedDatabaseContext)
        : base(scopedDatabaseContext)
    {
    }

    public Task<User> SelecionarUmaPor(Expression<Func<User, bool>> filtro = null,
        bool incluirRelacionamentos = true)
    {
        return ConsultaComIncludes(incluirRelacionamentos).Where(filtro).FirstOrDefaultAsync();
    }

    public Task<User> SelecionarPorDocumentoOuEmail(string documento, string email)
    {
        return SelecionarUmaPor(x =>
            x.Document == documento || x.Email == email
        );
    }
    
    public Task<User> SelecionarPorId(Guid id)
    {
        return SelecionarUmaPor(x =>
            x.Id == id
        );
    }

    public Task<User> ObterUsuarioPorEmailEPassword(string email, string password)
    {
        return SelecionarUmaPor(x =>
            x.Email == email && x.Password == password
        );
    }
}