using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class GroupData
    {
        public string Id { get; set; }
        public int Experience { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Id"] = Id,
                ["Experience"] = Experience
            };
        }
    }
}