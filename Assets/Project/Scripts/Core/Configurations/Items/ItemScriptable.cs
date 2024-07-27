using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [Serializable]
    public class ItemScriptable
    {
        [SerializeField] protected string name;
        [SerializeField] protected string key;
        [SerializeField] protected ItemType type;
        [SerializeField] protected LocalizedString nameLocale;
        [SerializeField] protected LocalizedString descriptionLocale;
        [SerializeField] protected SpriteAssetId spriteAssetId;

        public string Name { get => name; set => name = value; }
        public ItemType Type { get => type; set => type = value; }
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public SpriteAssetId SpriteAssetId { get => spriteAssetId; set => spriteAssetId = value; }
    }
}