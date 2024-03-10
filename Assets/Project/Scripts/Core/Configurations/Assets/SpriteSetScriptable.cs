using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "SpriteSet", menuName = "Scriptable/Assets/Sprite Set", order = 21)]
    public class SpriteSetScriptable : ScriptableObject
    {
        [SerializeField] private List<SpriteAssetScriptable> items;

        public List<SpriteAssetScriptable> Items => items;
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