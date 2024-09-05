using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks.Actions;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks;

public class RollbackActionsExecuter : IRollbackActionsExecuter
{
    private readonly IList<IRollbackAction> _rollbackActions;

    public RollbackActionsExecuter(IEnumerable<IRollbackAction> rollbackActions)
    {
        _rollbackActions = rollbackActions.ToArray();
    }

    public void ExecuteActions()
    {
        foreach (var action in _rollbackActions) action.Execute();
    }
}