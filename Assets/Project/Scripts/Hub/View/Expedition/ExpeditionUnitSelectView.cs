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
    public class ExpeditionUnitSelectView : MonoBehaviour
    {
        private const float NullAlpha = 0.25f;
        private const float NotNullAlpha = 1f;

        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IAssetsService _assetsService;
        [Inject] private readonly IPopupService _popupService;

        [SerializeField] private ExpeditionSlotId slotId;
        [SerializeField] private UnitSideId sideId;
        [SerializeField] private Color defaultBgColor;

        [Header("Plus")]
        [SerializeField] private GameObject plusWrapper;
        [SerializeField] private Button plusButton;
        [SerializeField] private List<UnitTypeId> types;

        [Header("Unit")]
        [SerializeField] private CanvasGroup unitCanvasGroup;
        [SerializeField] private GameObject unitWrapper;
        [SerializeField] private Button unitButton;
        [SerializeField] private Image unitIcon;
        [SerializeField] private Image unitBg;
        [SerializeField] private GameObject starWrapper;
        [SerializeField] private List<Image> stars;

        public UnitObject Data { get; set; }

        public ExpeditionSlotId SlotId => slotId;
        public UnitSideId SideId => sideId;

        private void Awake()
        {
            plusButton.onClick.AddListener(Click);
            unitButton.onClick.AddListener(Click);
        }

        private void Click()
        {
            if (sideId == UnitSideId.Enemy) return;

            var model = _popupService.Show<ExpeditionUnitSelectionViewModel>();
            model.CurrentIndex = slotId;
            model.UnitTypes = types;
        }

        public void SetData(UnitObject data)
        {
            Data = data;

            unitBg.color = data != null ? _configurations.GetUnitTypeData(Data.UnitTypeId).BgColor : defaultBgColor;
            unitIcon.sprite = data != null ? _assetsService.GetSprite(data.IconId) : null;
            unitIcon.gameObject.SetActive(data != null);
            starWrapper.SetActive(data != null);

            if (data != null)
            {
                for (var i = 0; i < stars.Count; i++)
                {
                    stars[i].gameObject.SetActive(i + 1 <= data.Star);
                    stars[i].color = unitBg.color;
                }
            }

            plusWrapper.SetActive(false);
            unitWrapper.SetActive(true);
            unitCanvasGroup.alpha = data != null ? NotNullAlpha : NullAlpha;
        }

        public void ResetData()
        {
            plusWrapper.SetActive(true);
            unitWrapper.SetActive(false);
        }
    }
}