using System;
using System.Linq;
using JetBrains.Annotations;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class ItemsUtil
    {
        public static int ParseItemKeyToStar(string key)
        {
            var stringStar = key.Split("|");
            return Convert.ToInt32(stringStar.Last());
        }
    }
}