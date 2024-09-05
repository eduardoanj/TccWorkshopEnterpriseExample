using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IRollbackActionsExecuter _rollbackActionsExecuter;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork, IRollbackActionsExecuter rollbackActionsExecuter)
    {
        _unitOfWork = unitOfWork;
        _rollbackActionsExecuter = rollbackActionsExecuter;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        using var transaction = await _unitOfWork.BeginTransaction();

        try
        {
            var result = await next();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await transaction.Rollback();
            _rollbackActionsExecuter.ExecuteActions();
            throw;
        }
    }
}