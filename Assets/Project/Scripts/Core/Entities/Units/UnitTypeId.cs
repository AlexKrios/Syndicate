using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class UnitTypeId : EntityId<string>, IEquatable<UnitTypeId>
    {
        public static readonly UnitTypeId All = new ("All");
        public static readonly UnitTypeId Trooper = new ("Trooper");
        public static readonly UnitTypeId Defender = new ("Defender");
        public static readonly UnitTypeId Support = new ("Support");
        public static readonly UnitTypeId Sniper = new ("Sniper");

        public UnitTypeId(string value) : base(value) { }

        public static explicit operator UnitTypeId(string value) => new(value);

        public static implicit operator string(UnitTypeId groupTypeId) => groupTypeId?.Value;

        public static bool operator ==(UnitTypeId a, UnitTypeId b) => a?.Value == b?.Value;

        public static bool operator !=(UnitTypeId a, UnitTypeId b) => !(a == b);

        public bool Equals(UnitTypeId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is UnitTypeId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}