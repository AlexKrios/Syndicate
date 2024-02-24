using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductGroupId : EntityId<string>, IEquatable<ProductGroupId>
    {
        public static readonly ProductGroupId Weapon = new ("Weapon");
        public static readonly ProductGroupId Armor = new ("Armor");

        public ProductGroupId(string value) : base(value) { }

        public static explicit operator ProductGroupId(string value) => new(value);

        public static implicit operator string(ProductGroupId groupId) => groupId?.Value;

        public static bool operator ==(ProductGroupId a, ProductGroupId b) => a?.Value == b?.Value;

        public static bool operator !=(ProductGroupId a, ProductGroupId b) => !(a == b);

        public bool Equals(ProductGroupId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ProductGroupId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}