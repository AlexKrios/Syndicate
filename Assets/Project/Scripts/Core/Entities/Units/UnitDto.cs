using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class UnitDto
    {
        public string Key { get; set; }
        public int Star { get; set; }
        public int Experience { get; set; }

        public Dictionary<ProductGroupId, string> Outfit { get; set; } = new();
    }
}