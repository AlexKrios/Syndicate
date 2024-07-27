using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class PartObject
    {
        [SerializeField] private string key;
        [SerializeField] private ItemType type;
        [SerializeField] private int count;

        public string Key { get => key; set => key = value; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get => type; set => type = value; }
        public int Count { get => count; set => count = value; }
    }
}