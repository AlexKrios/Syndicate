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

        public ItemTypeId ItemType => itemType;
        public string ItemId => itemId;
        public int Count => count;
    }
}