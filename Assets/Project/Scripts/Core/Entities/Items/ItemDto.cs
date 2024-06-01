using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemDto
    {
        public string Key { get; set; }
        public int Count { get; set; }
        public int Experience { get; set; }
    }
}