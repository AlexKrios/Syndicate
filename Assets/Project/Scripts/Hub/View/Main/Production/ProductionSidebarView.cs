using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
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
        [Inject] private readonly SignalBus _signalBus;
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
        [SerializeField] private Button create;

        [Space]
        [SerializeField] private Button queueButton;
        [SerializeField] private GameObject queueObject;

        private ICraftableItem _data;

        private void Awake()
        {
            create.onClick.AddListener(CreateClick);
            queueButton.onClick.AddListener(() => queueObject.SetActive(true));
            _signalBus.Subscribe<ProductionChangeSignal>(SetCreateButtonState);
        }

        private async void OnEnable()
        {
            //TODO Убрать асинхронность, когда запуск будет с прелодера
            await UniTask.Delay(1000);
            SetCreateButtonState();
        }

        public void SetData(ICraftableItem data)
        {
            _data = data;

            Refresh();
        }

        public void Refresh()
        {
            itemIcon.sprite = _assetsService.GetSprite(_data.SpriteAssetId);
            itemName.StringReference = _data.NameLocale;
            itemDescription.StringReference = _data.DescriptionLocale;

            var recipe = _data.Recipe;
            timerValue.text = TimeUtil.DateCraftTimer(recipe.CraftTime);
            var specificationsList = _data is ProductObject
                ? _specificationsUtil.GetProductSpecificationValues(_data)
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
                var partItemObject = _itemsProvider.GetItem(part.ItemType, part.Key);
                parts[i].SetData(partItemObject, part.Count);
            }
        }

        private async void CreateClick()
        {
            await _productionService.RemoveItems(_data);

            var productionObject = new ProductionObject
            {
                Id = Guid.NewGuid(),
                Key = _data.Key,
                Type = _data.ItemType,
                TimeEnd = DateTime.Now.AddSeconds(_data.Recipe.CraftTime).ToFileTime(),
                Index = _productionService.GetFreeCell()
            };

            _productionService.AddProduction(productionObject);

            _signalBus.Fire(new ProductionChangeSignal());
        }

        private void SetCreateButtonState()
        {
            create.interactable = _productionService.IsHaveFreeCell() && _productionService.IsHaveNeedItems(_data);
        }
    }
}