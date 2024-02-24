using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class AudioAssetId : EntityId<string>, IEquatable<AudioAssetId>
    {
        public AudioAssetId(string value) : base(value) { }

        public static explicit operator AudioAssetId(string value) => new(value);

        public static implicit operator string(AudioAssetId id) => id?.Value;

        public static bool operator ==(AudioAssetId a, AudioAssetId b) => a?.Value == b?.Value;

        public static bool operator !=(AudioAssetId a, AudioAssetId b) => !(a == b);

        public bool Equals(AudioAssetId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is AudioAssetId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}