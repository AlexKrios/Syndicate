using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemData
    {
        public ItemType ItemType { get; set; }
        public string Id { get; set; }
        public int Count { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            var result = new Dictionary<string, object>
            {
                ["ItemType"] = ItemType.ToString(),
                ["Id"] = Id,
                ["Count"] = Count
            };

            return result;
        }
    }
}