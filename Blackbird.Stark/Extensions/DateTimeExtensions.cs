using System;
using System.Collections.Generic;
using System.Globalization;

namespace Blackbird.Stark.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Returns dates of specified day of week, like all Mondays of the month
    /// </summary>
    /// <param name="self"></param>
    /// <param name="dayOfWeek"></param>
    /// <param name="calendar"></param>
    /// <returns></returns>
    public static IEnumerable<int> GetDatesOfDayOfWeek(this DateTime self, DayOfWeek dayOfWeek, Calendar calendar = null)
    {
        if (calendar == null) 
            calendar = CultureInfo.InvariantCulture.Calendar;
        var baseDate = new DateTime(self.Year, self.Month, 1, calendar);
        for (var i = 0; i < DateTime.DaysInMonth(self.Year, self.Month); i++)
        {
            var tmp = baseDate.AddDays(i);
            if (tmp.DayOfWeek == dayOfWeek)
                yield return tmp.Day;
        }
    }
}