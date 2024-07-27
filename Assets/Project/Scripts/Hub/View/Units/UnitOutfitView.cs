﻿using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class UnitOutfitView : MonoBehaviour
    {
        public Action<ProductGroupId, ProductObject> OnClickEvent { get; set; }

        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IProductsService _productsService;

        [SerializeField] private ProductGroupId group;
        [SerializeField] private Button button;

        [Space]
        [SerializeField] private GameObject freeWrapper;
        [SerializeField] private GameObject contentWrapper;

        [Space]
        [SerializeField] private Image icon;

        public ProductObject Data { get; private set; }

        public ProductGroupId Group => group;

        private void Awake()
        {
            button.onClick.AddListener(Click);
        }

        public void SetData(string outfit)
        {
            freeWrapper.SetActive(outfit == null);
            contentWrapper.SetActive(outfit != null);

            if (outfit != null)
            {
                Data = _productsService.GetProduct((ProductId) outfit);
                icon.sprite = _assetsService.GetSprite(Data.SpriteAssetId);
            }
            else
            {
                Data = null;
            }
        }

        private void Click()
        {
            OnClickEvent?.Invoke(group, Data);
        }
    }
}