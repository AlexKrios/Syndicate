using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "SpriteSet", menuName = "Scriptable/Assets/Sprite Set", order = -61)]
    public class SpriteSetScriptable : ScriptableObject
    {
        [SerializeField] private List<SpriteAssetScriptable> raw;
        [SerializeField] private List<SpriteAssetScriptable> weapon;
        [SerializeField] private List<SpriteAssetScriptable> armor;
        [SerializeField] private List<SpriteAssetScriptable> units;
        [SerializeField] private List<SpriteAssetScriptable> stars;

        public List<SpriteAssetScriptable> Raw => raw;
        public List<SpriteAssetScriptable> Weapon => weapon;
        public List<SpriteAssetScriptable> Armor => armor;
        public List<SpriteAssetScriptable> Units => units;
        public List<SpriteAssetScriptable> Stars => stars;

        public List<SpriteAssetScriptable> GetAllSprites()
        {
            var allValues = new List<SpriteAssetScriptable>();
            allValues.AddRange(raw);
            allValues.AddRange(weapon);
            allValues.AddRange(armor);
            allValues.AddRange(units);

            return allValues;
        }
    }

    [Serializable]
    public class SpriteAssetScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;

        public SpriteAssetId Id => (SpriteAssetId)id;
        public Sprite Sprite => sprite;
    }
}