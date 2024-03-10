using System;

namespace Syndicate.Utils
{
    public class TimeUtil
    {
        public static string DateCraftTimer(int time)
        {
            var result = TimeSpan.FromSeconds(time);
            return time switch
            {
                < 3599 => result.ToString("mm':'ss"),
                >= 3600 and < 86400 => result.ToString("hh':'mm':'ss"),
                _ => string.Empty
            };
        }
    }
}