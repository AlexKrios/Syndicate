using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemData
    {
        public string Id { get; set; }
        public int Count { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Id"] = Id,
                ["Count"] = Count
            };
        }
    }
}