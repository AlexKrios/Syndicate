using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductionDto
    {
        public Guid id;
        public string key;
        public string type;
        public long timeEnd;
    }
}