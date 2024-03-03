using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class HeroTypeId : EntityId<string>, IEquatable<HeroTypeId>
    {
        public static readonly HeroTypeId All = new ("All");
        public static readonly HeroTypeId Trooper = new ("Trooper");
        public static readonly HeroTypeId Defender = new ("Defender");
        public static readonly HeroTypeId Support = new ("Support");
        public static readonly HeroTypeId Sniper = new ("Sniper");

        public HeroTypeId(string value) : base(value) { }

        public static explicit operator HeroTypeId(string value) => new(value);

        public static implicit operator string(HeroTypeId groupTypeId) => groupTypeId?.Value;

        public static bool operator ==(HeroTypeId a, HeroTypeId b) => a?.Value == b?.Value;

        public static bool operator !=(HeroTypeId a, HeroTypeId b) => !(a == b);

        public bool Equals(HeroTypeId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is HeroTypeId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}