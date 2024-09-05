namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

public interface IUnitOfWorkTransaction : IDisposable
{
    Task CommitAsync();
    Task Rollback();
}