namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<IUnitOfWorkTransaction> BeginTransaction();
}