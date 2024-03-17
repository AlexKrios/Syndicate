using System;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class PartObject
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private string key;
        [SerializeField] private int count;

        public ItemType ItemType { get => itemType; set => itemType = value; }
        public string Key { get => key; set => key = value; }
        public int Count { get => count; set => count = value; }
    }
}