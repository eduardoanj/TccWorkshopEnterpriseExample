using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Contexts;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    public UnitOfWork(IScopedDatabaseContext scopedContext)
    {
        _dbContext = scopedContext.Context;
    }

    public async Task<IUnitOfWorkTransaction> BeginTransaction()
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        return new UnitOfWorkTransaction(_dbContext, transaction);
    }
}