using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class LocationId : EntityId<string>, IEquatable<LocationId>
    {
        public LocationId(string value) : base(value) { }

        public static explicit operator LocationId(string value) => new(value);

        public static implicit operator string(LocationId id) => id?.Value;

        public static bool operator ==(LocationId a, LocationId b) => a?.Value == b?.Value;

        public static bool operator !=(LocationId a, LocationId b) => !(a == b);

        public bool Equals(LocationId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is LocationId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}