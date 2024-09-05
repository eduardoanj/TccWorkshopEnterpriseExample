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
    private readonly IClock _clock;
    protected readonly DatabaseContext Context;
    protected readonly DbSet<T> Entity;

    protected BaseRepository(IScopedDatabaseContext scopedDatabaseContext, 
        IClock clock)
    {
        _clock = clock;
        Context = scopedDatabaseContext.Context;
        Entity = Context.Set<T>();
    }

    public virtual async Task Inserir(T entidade)
    {
        entidade.CreatedAt = _clock.Now;
        entidade.LastModifiedAt = _clock.Now;
        await Entity.AddAsync(entidade);
    }

    public virtual Task Atualizar(T entidade)
    {
        entidade.LastModifiedAt = _clock.Now;
        Entity.Update(entidade);
        return Task.CompletedTask;
    }

    public Task InserirOuAtualizar(T entidade)
    {
        return Entity.Any(e => e.Id == entidade.Id) ? Atualizar(entidade) : Inserir(entidade);
    }

    public Task ExcluirFisicamente(T entidade)
    {
        Entity.Remove(entidade);
        return Task.CompletedTask;
    }

    public Task ExcluirLogicamente(T entidade)
    {
        entidade.LastModifiedAt = _clock.Now;
        entidade.Deleted = true;

        return Atualizar(entidade);
    }

    public virtual async Task<IList<T>> SelecionarVariasPor(Expression<Func<T, bool>> filtro = null)
    {
        if (filtro == null)
            return await Entity.ToListAsync();

        return await Entity.Where(filtro).ToListAsync();
    }

    public virtual async Task<T> SelecionarUmaPor(Expression<Func<T, bool>> filtro = null)
    {
        return await Entity.SingleOrDefaultAsync(filtro!);
    }

    public Task<T> ObterPorId(Guid id)
    {
        return SelecionarUmaPor(x => x.Id == id);
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