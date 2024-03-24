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
        [SerializeField] protected string id;
        [SerializeField] protected LocalizedString nameLocale;
        [SerializeField] protected LocalizedString descriptionLocale;
        [SerializeField] protected SpriteAssetId spriteAssetId;
        [SerializeField] private RecipeObject recipe;

        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public SpriteAssetId SpriteAssetId { get => spriteAssetId; set => spriteAssetId = value; }
        public RecipeObject Recipe => recipe;
    }
}