using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class StorageSidebarView : MonoBehaviour
    {
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Image itemIcon;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private List<StorageSpecView> specifications;

        public void SetData(ICraftableItem data)
        {
            itemIcon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
            itemName.StringReference = data.NameLocale;
            itemDescription.StringReference = data.DescriptionLocale;

            var recipe = data.Recipe;
            var specificationsList = data is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(data)
                : recipe.Specifications;
            SetSpecificationData(specificationsList);
        }

        private void SetSpecificationData(IReadOnlyCollection<SpecificationObject> specificationsList)
        {
            foreach (var specification in specifications)
            {
                var needSpecification = specificationsList.FirstOrDefault(x => x.Type == specification.Id);
                if (needSpecification == null)
                {
                    specification.ResetData();
                    continue;
                }

                specification.SetData(needSpecification);
            }
        }
    }
}