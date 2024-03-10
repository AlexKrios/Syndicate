using System;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class PartObject
    {
        [SerializeField] private ItemTypeId itemType;
        [SerializeField] private string itemId;
        [SerializeField] private int count;

        public ItemTypeId ItemType { get => itemType; set => itemType = value; }
        public string ItemId { get => itemId; set => itemId = value; }
        public int Count { get => count; set => count = value; }
    }
}