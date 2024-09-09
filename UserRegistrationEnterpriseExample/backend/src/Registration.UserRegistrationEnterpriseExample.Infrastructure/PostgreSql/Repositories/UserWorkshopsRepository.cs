using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Repositories;

public class UserWorkshopsRepository : BaseRepository<UserWorkshop>, IUserWorkshops
{
    public UserWorkshopsRepository(IScopedDatabaseContext scopedDatabaseContext) : base(scopedDatabaseContext)
    {
    }

    public async Task<List<UserWorkshop>> ObterVariasPorUserId(Guid userId)
    {
        return ConsultaComIncludes(true)
            .Where(x => x.UserId == userId)
            .ToList();
    }
    
    public async Task Vincular(UserWorkshop userWorkshop)
    {
        await Inserir(userWorkshop);
    }
}