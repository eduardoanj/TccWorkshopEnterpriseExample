using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Behaviors;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application.Common.Behaviours;

public class RequestValidationBehaviorTests
{
    private RequestValidationBehavior<SampleRequestClassForTests, SampleRequestViewModelForTests>
        _requestValidationBehavior;

    [Fact]
    public void Should_not_throw_exception_when_there_are_not_validation_failures()
    {
        var calledNext = false;

        var validators = Substitute.For<IEnumerable<IValidator<SampleRequestClassForTests>>>();

        _requestValidationBehavior =
            new RequestValidationBehavior<SampleRequestClassForTests, SampleRequestViewModelForTests>(validators);

        Func<Task> func = async () => await _requestValidationBehavior
            .Handle(new SampleRequestClassForTests(), CancellationToken.None, () =>
            {
                calledNext = true;
                return Task.FromResult(new SampleRequestViewModelForTests());
            });

        func.Should().NotThrowAsync<Exception>();
        calledNext.Should().BeTrue();
    }

    [Fact]
    public void Should_throw_exception_when_there_are_validation_failures()
    {
        var calledNext = false;

        var validators = Substitute.For<IEnumerable<IValidator<SampleRequestClassForTests>>>();

        _requestValidationBehavior =
            new RequestValidationBehavior<SampleRequestClassForTests, SampleRequestViewModelForTests>(validators);

        Func<Task> func = async () => await _requestValidationBehavior
            .Handle(new SampleRequestClassForTests(), CancellationToken.None,
                () => throw new ValidationException(string.Empty));

        func.Should().ThrowAsync<ValidationException>();
        calledNext.Should().BeFalse();
    }
}