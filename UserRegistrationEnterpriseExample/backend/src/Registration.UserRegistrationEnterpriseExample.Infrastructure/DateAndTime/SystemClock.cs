using Registration.UserRegistrationEnterpriseExample.Domain.Common;
using TimeZoneConverter;

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.DateAndTime;

public class SystemClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
    public DateTime Today => DateTime.UtcNow.Date;

    public DateTime NowTZ(TimeZoneInfo timeZoneInfo)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(Now, timeZoneInfo);
    }

    public DateTime NowTZ(string timeZone)
    {
        var timeZoneInfo = TZConvert.GetTimeZoneInfo(timeZone);
        return NowTZ(timeZoneInfo);
    }
}