using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using Syndicate.Utils;
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
        [SerializeField] private Image star;

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

            var starCount = ItemsUtil.ParseItemKeyToStar(data.Key);
            star.sprite = _assetsService.GetStarSprite(starCount);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}