using Syndicate.Core.Configurations;
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
    public class ProductionQueueUpgradeView : PopupViewBase<ProductionQueueUpgradeViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IProductionService _productionService;

        [SerializeField] private Button close;
        [SerializeField] private RequestButton upgrade;
        [SerializeField] private TMP_Text costText;

        private ProductionUpgradeScriptable UpgradeData => _configurationsScriptable.ProductionSet[_productionService.Size];

        private void Awake()
        {
            close.onClick.AddListener(Close);
            upgrade.Button.onClick.AddListener(UpgradeClick);
        }

        protected override void OnEnable()
        {
            costText.text = string.Format(Constants.CashPattern, UpgradeData.Cost);
            upgrade.Button.interactable = _gameService.Cash >= UpgradeData.Cost;

            base.OnEnable();
        }

        private async void UpgradeClick()
        {
            await _productionService.AddProductionSize(UpgradeData.Cost);

            _signalBus.Fire<ProductionSizeChangeSignal>();
            ViewModel.Hide?.Invoke();
        }
    }
}