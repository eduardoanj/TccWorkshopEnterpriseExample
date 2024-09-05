using System.Globalization;
using System.Text.RegularExpressions;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Extensions;

public static class StringExtension
{
    public static DateTime ToIsoDateTime(this string value)
    {
        var result = HasMicroseconds(value) ? value[..^3] : value;
        return DateTime.Parse(result, CultureInfo.InvariantCulture);
    }

    private static bool HasMicroseconds(string value)
    {
        return Regex.IsMatch(value, @".\d{6}");
    }
}