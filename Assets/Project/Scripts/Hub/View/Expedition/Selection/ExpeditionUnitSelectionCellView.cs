using System;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class ExpeditionUnitSelectionCellView : ButtonWithActiveBorder
    {
        public Action<ExpeditionUnitSelectionCellView> OnClickEvent { get; set; }

        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;

        [SerializeField] private Button button;

        [Space]
        [SerializeField] private Image background;
        [SerializeField] private Image icon;

        public UnitObject Data { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            button.onClick.AddListener(Click);
        }

        public void SetData(UnitObject data)
        {
            Data = data;

            icon.sprite = _assetsService.GetSprite(data.IconId);
            background.color = _configurations.GetUnitTypeData(Data.UnitTypeId).BgColor;
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}