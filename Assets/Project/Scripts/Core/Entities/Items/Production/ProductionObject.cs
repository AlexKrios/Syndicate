using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductionObject
    {
        public Guid Guid { get; set; }
        public string Key { get; set; }
        public ItemType Type { get; set; }

        public long TimeEnd { get; set; }
        public int Index { get; set; }

        [JsonIgnore]
        public ICraftableItem Preset { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Guid"] = Guid.ToString(),
                ["Key"] = Key,
                ["Type"] = Type.ToString(),
                ["TimeEnd"] = TimeEnd,
                ["Index"] = Index
            };
        }
    }
}