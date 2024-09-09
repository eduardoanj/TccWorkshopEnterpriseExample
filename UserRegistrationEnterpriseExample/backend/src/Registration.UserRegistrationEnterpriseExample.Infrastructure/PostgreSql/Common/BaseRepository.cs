using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Database;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public abstract class BaseRepository<T> : IBaseRepository<T>
    where T : EntidadeAuditavel
{
    protected readonly DatabaseContext Context;
    protected readonly DbSet<T> Entity;

    protected BaseRepository(IScopedDatabaseContext scopedDatabaseContext)
    {
        Context = scopedDatabaseContext.Context;
        Entity = Context.Set<T>();
    }

    public virtual async Task Inserir(T entidade)
    {
        entidade.CreatedAt = DateTime.Now;
        entidade.LastModifiedAt = DateTime.Now;
        await Entity.AddAsync(entidade);
        await Context.SaveChangesAsync();
    }

    public virtual Task Atualizar(T entidade)
    {
        entidade.LastModifiedAt = DateTime.Now;
        Entity.Update(entidade);
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task InserirOuAtualizar(T entidade)
    {
        return Entity.Any(e => e.Id == entidade.Id) ? Atualizar(entidade) : Inserir(entidade);
    }
    
    public virtual async Task<T> SelecionarUmaPor(Expression<Func<T, bool>> filtro = null)
    {
        return await Entity.SingleOrDefaultAsync(filtro!);
    }
    
    public IQueryable<T> ConsultaComIncludes(bool eager = false)
    {
        var query = Entity.AsQueryable();

        if (!eager) return query.AsSplitQuery();

        var navigations = Context.Model.FindEntityType(typeof(T))
            .GetDerivedTypesInclusive()
            .SelectMany(type => type.GetNavigations())
            .Distinct();

        query = navigations.Aggregate(query, (current, property) => current.Include(property.Name));

        return query.AsSplitQuery();
    }
}