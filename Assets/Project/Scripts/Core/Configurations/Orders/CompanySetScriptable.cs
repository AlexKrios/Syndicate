using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "CompanySet", menuName = "Scriptable/Company Set", order = 51)]
    public class CompanySetScriptable : ListScriptableObject<CompanyScriptable> { }

    [Serializable]
    public class CompanyScriptable
    {
        [SerializeField] private CompanyId key;
        [SerializeField] private LocalizedString nameLocale;
        [SerializeField] private SpriteAssetId iconAssetId;

        public CompanyId Key => key;
        public LocalizedString NameLocale => nameLocale;
        public SpriteAssetId IconAssetId => iconAssetId;
    }
}