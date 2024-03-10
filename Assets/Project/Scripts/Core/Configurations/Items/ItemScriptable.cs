using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [Serializable]
    public class ItemScriptable
    {
        [SerializeField] protected string id;
        [SerializeField] protected LocalizedString nameLocale;
        [SerializeField] protected LocalizedString descriptionLocale;
        [SerializeField] protected SpriteAssetId spriteAssetId;
        [SerializeField] protected RecipeObject recipe;

        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public SpriteAssetId SpriteAssetId => spriteAssetId;
        public RecipeObject Recipe => recipe;
    }
}