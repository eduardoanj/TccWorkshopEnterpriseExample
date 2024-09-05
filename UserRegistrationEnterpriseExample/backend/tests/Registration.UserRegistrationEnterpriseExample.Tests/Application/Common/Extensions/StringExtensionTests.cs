using FluentAssertions;
using FluentAssertions.Extensions;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Extensions;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application.Common.Extensions;

public class StringExtensionTests
{
    [Fact]
    public void Should_return_an_iso_datetime_from_datetime_string_that_has_microseconds()
    {
        "2020-09-02T20:20:40.000000".ToIsoDateTime().Should()
            .Be(2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc());
    }

    [Fact]
    public void Should_return_an_iso_datetime_from_datetime_string_that_has_not_microseconds()
    {
        "2020-09-02T20:20:40.000".ToIsoDateTime().Should()
            .Be(2.September(2020).At(20.Hours(20.Minutes(40.Seconds()))).AsUtc());
    }
}