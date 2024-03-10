using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemTypeId : EntityId<string>, IEquatable<ItemTypeId>
    {
        public static readonly ItemTypeId Raw = new("Raw");
        public static readonly ItemTypeId Component = new("Component");
        public static readonly ItemTypeId Product = new("Product");

        public ItemTypeId(string value) : base(value) { }

        public static explicit operator ItemTypeId(string value) => new(value);

        public static implicit operator string(ItemTypeId groupId) => groupId?.Value;

        public static bool operator ==(ItemTypeId a, ItemTypeId b) => a?.Value == b?.Value;

        public static bool operator !=(ItemTypeId a, ItemTypeId b) => !(a == b);

        public bool Equals(ItemTypeId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ItemTypeId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}