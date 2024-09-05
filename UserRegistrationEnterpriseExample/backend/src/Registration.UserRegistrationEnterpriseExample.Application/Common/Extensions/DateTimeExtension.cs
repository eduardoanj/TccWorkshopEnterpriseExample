namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Extensions;

public static class DateTimeExtension
{
    public static bool IsOlderThan(this DateTime valueA, DateTime? valueB)
    {
        if (!valueB.HasValue) return false;
        return valueA < valueB.Value;
    }

    public static string ToIsoDateTimeString(this DateTime value)
    {
        return value.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
    }
}