using Cysharp.Threading.Tasks;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View
{
    public class OrderCompanyUpgradeView : PopupViewBase<OrderCompanyUpgradeViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IOrdersService _ordersService;

        [SerializeField] private Button close;
        [SerializeField] private RequestButton upgrade;
        [SerializeField] private TMP_Text costText;

        private void Awake()
        {
            close.onClick.AddListener(Close);
            upgrade.Button.onClick.AddListener(UpgradeClick);
        }

        protected override async void OnEnable()
        {
            await UniTask.Yield();

            costText.text = string.Format(Constants.CashPattern, ViewModel.UpgradeData.Cost);
            upgrade.Button.interactable = _gameService.Cash >= ViewModel.UpgradeData.Cost;

            base.OnEnable();
        }

        private async void UpgradeClick()
        {
            await _ordersService.AddOrderCompany(ViewModel.CompanyId, ViewModel.UpgradeData.Cost);

            _signalBus.Fire<OrdersChangeSignal>();
            ViewModel.Hide?.Invoke();
        }
    }
}