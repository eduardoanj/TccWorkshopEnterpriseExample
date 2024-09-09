using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Repositories;

public class WorkshopsRepository : BaseRepository<WorkShop>, IWorkshops
{
    public WorkshopsRepository(IScopedDatabaseContext scopedDatabaseContext) : base(scopedDatabaseContext)
    {
    }

    public Task<WorkShop> SelecionarUmaPor(Expression<Func<WorkShop, bool>> filtro = null, bool incluirRelacionamentos = true)
    {
        return ConsultaComIncludes(incluirRelacionamentos).Where(filtro).FirstOrDefaultAsync();
    }

    public Task<WorkShop> SelecionarPorId(Guid id)
    {
        return SelecionarUmaPor(x =>
            x.Id == id
        );
    }

    public async Task<(IList<WorkShop>, int)> ObterWorkshopsFiltrados(GetWorkshopRequest request)
    {
        var workshops = Entity as IQueryable<WorkShop>;
        
        var workShopFiltrados = ObterWorkShopFiltrados(request, workshops);
        
        var items = await workShopFiltrados
            .Skip(ToSkip(request))
            .Take(request.QtdePorPagina)
            .ToListAsync();

        var totalItems = await workShopFiltrados.CountAsync();

        return (items, totalItems);
    }
    
    private static int ToSkip(GetWorkshopRequest request)
    {
        return (request.Pagina - 1) * request.QtdePorPagina;
    }
    
    private IQueryable<WorkShop> ObterWorkShopFiltrados(GetWorkshopRequest request, IQueryable<WorkShop> workShops)
    {
        var predicate = PredicateBuilder.New<WorkShop>(defaultExpression: true);

        if (!string.IsNullOrEmpty(request.IdCreator))
        {
            predicate = predicate.And(c => c.IdCreator == request.IdCreator);
        }

        
        predicate = predicate.And(c => c.Date > request.DataInicioFiltro);
        predicate = predicate.And(c => c.Date < request.DataFinalFiltro);

        var consulta = workShops
            .Where(predicate);

        return consulta;
    }
}