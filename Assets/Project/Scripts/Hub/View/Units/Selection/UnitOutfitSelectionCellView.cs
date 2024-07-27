using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class UnitOutfitSelectionCellView : ButtonWithActiveBorder
    {
        public Action<UnitOutfitSelectionCellView> OnClickEvent { get; set; }

        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Button button;

        [Space]
        [SerializeField] private Image icon;

        public ProductObject Data { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            button.onClick.AddListener(Click);
        }

        public void SetData(ProductObject data)
        {
            Data = data;

            icon.sprite = _assetsService.GetSprite(data.SpriteAssetId);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}