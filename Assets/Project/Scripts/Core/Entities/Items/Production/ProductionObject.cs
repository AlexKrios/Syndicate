using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductionObject
    {
        public Guid Id { get; set; }
        public string Key { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }

        public long TimeEnd { get; set; }
        public int Index { get; set; }

        [JsonIgnore] public ICraftableItem ItemRef { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Id"] = Id.ToString(),
                ["Key"] = Key,
                ["Type"] = Type.ToString(),
                ["TimeEnd"] = TimeEnd,
                ["Index"] = Index
            };
        }
    }
}