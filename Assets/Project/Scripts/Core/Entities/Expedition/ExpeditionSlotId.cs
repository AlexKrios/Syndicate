using System;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ExpeditionSlotId : EntityId<string>, IEquatable<ExpeditionSlotId>
    {
        public static readonly ExpeditionSlotId ForwardSlot1 = new("ForwardSlot1");
        public static readonly ExpeditionSlotId ForwardSlot2 = new("ForwardSlot2");
        public static readonly ExpeditionSlotId BackSlot1 = new("BackSlot1");
        public static readonly ExpeditionSlotId BackSlot2 = new("BackSlot2");

        public ExpeditionSlotId(string value) : base(value) { }

        public static explicit operator ExpeditionSlotId(string value) => new(value);

        public static implicit operator string(ExpeditionSlotId slotId) => slotId?.Value;

        public static bool operator ==(ExpeditionSlotId a, ExpeditionSlotId b) => a?.Value == b?.Value;

        public static bool operator !=(ExpeditionSlotId a, ExpeditionSlotId b) => !(a == b);

        public bool Equals(ExpeditionSlotId other) => Value != null && Value.Equals(other?.Value);

        public override bool Equals(object obj) => obj is ExpeditionSlotId id && Equals(id);

        public override int GetHashCode() => Value.GetHashCode();
    }
}