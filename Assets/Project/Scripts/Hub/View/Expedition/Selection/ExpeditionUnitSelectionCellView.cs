using System;
using System.Collections.Generic;
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
        [SerializeField] private List<Image> stars;

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
            for (var i = 0; i < stars.Count; i++)
            {
                stars[i].gameObject.SetActive(i + 1 <= data.Star);
                stars[i].color = background.color;
            }
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}