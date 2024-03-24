using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public enum ItemType
    {
        Raw = 0,
        Component = 1,
        Product = 2,

        RawGroup = 10,
        ComponentGroup = 11,
        ProductGroup = 12
    }
}