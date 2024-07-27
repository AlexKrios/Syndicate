using System;
using JetBrains.Annotations;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class DateUtil
    {
        public static string DateCraftTimer(int time)
        {
            var result = TimeSpan.FromSeconds(time);
            if (result.Hours > 0) 
                return $"{result.Hours:#0}:{result.Minutes:00}:{result.Seconds:00}";
            
            if (result.Minutes > 0)
                return $"{result.Minutes:#0}:{result.Seconds:00}";

            return $"{result.Seconds:#0}";
        }

        public static int GetTime(long time)
        {
            var timeEnd = DateTime.FromFileTime(time);
            
            if (DateTime.Now < timeEnd)
                return (int)(timeEnd - DateTime.Now).TotalSeconds;

            return 0;
        }
        
        public static DateTime StartOfTheDay(DateTime d) => new(d.Year, d.Month, d.Day, 0, 0,0);
        public static DateTime EndOfTheDay(DateTime d) => new(d.Year, d.Month, d.Day, 23, 59,59);

        public static DateTime IntervalHourTime(int interval)
        {
            var step = DateTime.Now.Hour / interval + 1;
            var nextHour = step * interval;

            var d = nextHour == 24 ? DateTime.UtcNow.AddDays(1) : DateTime.Now;
            nextHour = nextHour == 24 ? 0 : nextHour;
            return new DateTime(d.Year, d.Month, d.Day, nextHour, 0,0);
        }

    }
}