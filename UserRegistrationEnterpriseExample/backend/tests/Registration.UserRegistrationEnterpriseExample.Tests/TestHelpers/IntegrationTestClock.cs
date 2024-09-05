using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Domain.Common;

namespace Registration.UserRegistrationEnterpriseExample.Tests.TestHelpers;

public class IntegrationTestClock : IClock
{
    private readonly IClock _mockedClock;

    public IntegrationTestClock(DateTime seedDateTime)
    {
        _mockedClock = Substitute.For<IClock>();
        SetIntegrationClock(seedDateTime);
    }

    public DateTime Now => _mockedClock.Now;

    public void AdvanceBy(TimeSpan timeSpan)
    {
        SetIntegrationClock(_mockedClock.Now + timeSpan);
    }

    private void SetIntegrationClock(DateTime dateTime)
    {
        _mockedClock.Now.Returns(dateTime);
    }
}