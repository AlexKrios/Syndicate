using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class MusicAssetId : EntityId<string>, IEquatable<MusicAssetId>
    {
        public MusicAssetId(string value) : base(value) { }

        public static explicit operator MusicAssetId(string value) => new(value);

        public static implicit operator string(MusicAssetId id) => id?.Value;

        public static bool operator ==(MusicAssetId a, MusicAssetId b) => a?.Value == b?.Value;

        public static bool operator !=(MusicAssetId a, MusicAssetId b) => !(a == b);

        public bool Equals(MusicAssetId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is MusicAssetId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}