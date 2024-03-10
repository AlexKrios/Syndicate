using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ProductId : EntityId<string>, IEquatable<ProductId>
    {
        public ProductId(string value) : base(value) { }

        public static explicit operator ProductId(string value) => new(value);

        public static implicit operator string(ProductId groupId) => groupId?.Value;

        public static bool operator ==(ProductId a, ProductId b) => a?.Value == b?.Value;

        public static bool operator !=(ProductId a, ProductId b) => !(a == b);

        public bool Equals(ProductId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ProductId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}