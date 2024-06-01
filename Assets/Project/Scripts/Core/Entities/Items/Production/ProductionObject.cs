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

        public long TimeEnd { get; set; }
        public int Index { get; set; }

        [JsonIgnore] public ICraftableItem ItemRef { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Guid"] = Guid.ToString(),
                ["Key"] = Key,
                ["TimeEnd"] = TimeEnd,
                ["Index"] = Index
            };
        }
    }
}