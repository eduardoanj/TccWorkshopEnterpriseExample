using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors.Rollbacks.Actions;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application.Common.Behaviours.Rollbacks;

public class RollbackActionsTests
{
    [Fact]
    public void It_should_execute_all_rollback_actions()
    {
        var actions = new List<IRollbackAction>
        {
            Substitute.For<IRollbackAction>(),
            Substitute.For<IRollbackAction>()
        };
        var rollbackActions = new RollbackActionsExecuter(actions);

        rollbackActions.ExecuteActions();

        foreach (var action in actions) action.Received().Execute();
    }
}