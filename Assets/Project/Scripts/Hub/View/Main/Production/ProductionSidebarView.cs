using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionSidebarView : MonoBehaviour
    {
        public Action<ItemObject> OnCreateClick { get; set; }

        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IItemsProvider _itemsProvider;
        [Inject] private readonly IProductionService _productionService;
        [Inject] private readonly SpecificationsUtil _specificationsUtil;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text timerValue;
        [SerializeField] private LocalizeStringEvent itemName;
        [SerializeField] private LocalizeStringEvent itemDescription;
        [SerializeField] private List<ProductionSpecView> specifications;
        [SerializeField] private List<ProductionPartView> parts;

        private void OnEnable()
        {
            OnCreateClick += CreateClick;
        }

        private void OnDisable()
        {
            OnCreateClick -= CreateClick;
        }

        public void SetData(ItemObject itemObject)
        {
            itemIcon.sprite = _assetsService.GetSprite(itemObject.SpriteAssetId);
            itemName.StringReference = itemObject.NameLocale;
            itemDescription.StringReference = itemObject.DescriptionLocale;

            var recipe = itemObject.Recipe;
            timerValue.text = TimeUtil.DateCraftTimer(recipe.CraftTime);
            var specificationsList = itemObject is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(recipe.Parts)
                : recipe.Specifications;
            SetSpecificationData(specificationsList);
            SetPartsData(recipe.Parts);
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

        private void SetPartsData(IReadOnlyList<PartObject> partObjects)
        {
            for (var i = 0; i < parts.Count; i++)
            {
                if (partObjects.ElementAtOrDefault(i) == null)
                {
                    parts[i].SetData(null);
                    continue;
                }

                var part = partObjects[i];
                var partItemObject = _itemsProvider.GetItem(part.ItemType, part.ItemId);
                parts[i].SetData(partItemObject, part.Count);
            }
        }

        private void CreateClick(ItemObject itemObject)
        {
            var productionObject = new ProductionObject
            {
                Id = Guid.NewGuid(),
                Key = itemObject.id,
                Type = itemObject.ItemTypeId,
                TimeEnd = DateTime.Now.AddSeconds(itemObject.Recipe.CraftTime).ToFileTime()
            };

            _productionService.AddProduction(productionObject);
        }
    }
}