using System;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    [RequireComponent(typeof(Button))]
    public class OrderCompanyTabView : ButtonWithActiveBorder
    {
        [Inject] private readonly IPopupService _popupService;

        [SerializeField] private CompanyId type;
        [SerializeField] private Image icon;
        [SerializeField] private Button buyButton;

        [Space]
        [SerializeField] private GameObject activeWrapper;
        [SerializeField] private GameObject buyWrapper;

        public CompanyId Type => type;

        public Action<OrderCompanyTabView> OnClickEvent { get; set; }

        private OrderGroupUpgradeScriptable _upgradeData;

        protected override void Awake()
        {
            base.Awake();

            buyButton.onClick.AddListener(BuyButtonClick);
        }

        private void OnDisable()
        {
            OnClickEvent = null;
        }

        public void SetStateActive(Sprite sprite)
        {
            icon.sprite = sprite;

            SetState(true);
        }

        public void SetStateBuy(OrderGroupUpgradeScriptable data, bool isAvailable)
        {
            _upgradeData = data;
            buyButton.gameObject.SetActive(isAvailable);

            SetState(false);
        }

        private void SetState(bool value)
        {
            activeWrapper.SetActive(value);
            buyWrapper.SetActive(!value);
        }

        protected override void Click()
        {
            OnClickEvent?.Invoke(this);
        }

        private void BuyButtonClick()
        {
            var model = _popupService.Get<OrderCompanyUpgradeViewModel>();
            model.UpgradeData = _upgradeData;
            model.CompanyId = type;

            _popupService.Show<OrderCompanyUpgradeViewModel>();
        }
    }
}