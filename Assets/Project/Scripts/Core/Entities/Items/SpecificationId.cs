using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class SpecificationId : EntityId<string>, IEquatable<SpecificationId>
    {
        public static readonly SpecificationId Attack = new ("Attack");
        public static readonly SpecificationId Health = new ("Health");
        public static readonly SpecificationId Defense = new ("Defense");
        public static readonly SpecificationId Initiative = new ("Initiative");

        public SpecificationId(string value) : base(value) { }

        public static explicit operator SpecificationId(string value) => new(value);

        public static implicit operator string(SpecificationId groupId) => groupId?.Value;

        public static bool operator ==(SpecificationId a, SpecificationId b) => a?.Value == b?.Value;

        public static bool operator !=(SpecificationId a, SpecificationId b) => !(a == b);

        public bool Equals(SpecificationId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is SpecificationId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}