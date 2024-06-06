using System;
using System.Collections.Generic;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ExpeditionObject
    {
        public Guid Guid { get; set; }
        public string Key { get; set; }

        public long TimeEnd { get; set; }
        public int Index { get; set; }

        public Dictionary<string, string> Roster { get; set; } = new();
    }
}