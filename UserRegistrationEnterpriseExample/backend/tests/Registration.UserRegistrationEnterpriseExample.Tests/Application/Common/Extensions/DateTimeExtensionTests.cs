using FluentAssertions;
using FluentAssertions.Extensions;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Extensions;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application.Common.Extensions;

public class DateTimeExtensionTests
{
    [Fact]
    public void DateTimeA_should_be_older_than_DateTimeB()
    {
        var dateTimeA = 2.September(2020);
        var dateTimeB = 2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc();

        dateTimeA.IsOlderThan(dateTimeB).Should().BeTrue();
    }

    [Fact]
    public void DateTimeA_should_be_newer_than_DateTimeB()
    {
        var dateTimeA = 2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc();
        var dateTimeB = 2.September(2020);

        dateTimeA.IsOlderThan(dateTimeB).Should().BeFalse();
    }

    [Fact]
    public void DateTimeA_should_be_the_same_as_DateTimeB()
    {
        var dateTimeA = 2.September(2020);
        var dateTimeB = 2.September(2020);

        dateTimeA.IsOlderThan(dateTimeB).Should().BeFalse();
    }

    [Fact]
    public void Should_return_false_when_there_is_not_another_datetime()
    {
        var dateTime = 2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc();

        dateTime.IsOlderThan(null).Should().BeFalse();
    }

    [Fact]
    public void Should_return_the_datetime_as_an_iso_datetime_string()
    {
        var dateTime = 2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc();

        dateTime.ToIsoDateTimeString().Should().Be("2020-09-02T20:20:40.000000");
    }
}