using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class GroupData
    {
        public ItemType ItemType { get; set; }
        public string Id { get; set; }
        public int Experience { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["ItemTypeId"] = ItemType.ToString(),
                ["Id"] = Id,
                ["Experience"] = Experience
            };
        }
    }
}