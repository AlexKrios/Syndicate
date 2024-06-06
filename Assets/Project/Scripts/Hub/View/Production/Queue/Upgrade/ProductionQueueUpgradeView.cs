using Syndicate.Core.Configurations;
using Syndicate.Core.Profile;
using Syndicate.Core.Services;
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
        private const string CashPattern = "<sprite name=\"Cash\"> {0}";

        [Inject] private readonly ConfigurationsScriptable _configurationsScriptable;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IProductionService _productionService;

        [SerializeField] private Button close;
        [SerializeField] private RequestButton upgrade;
        [SerializeField] private TMP_Text costText;

        private PlayerState PlayerState => _gameService.GetPlayerState();

        private void Awake()
        {
            close.onClick.AddListener(Close);
            upgrade.Button.onClick.AddListener(UpgradeClick);
        }

        protected override void OnEnable()
        {
            var productionData = _configurationsScriptable.ProductionSet[_productionService.Size];
            costText.text = string.Format(CashPattern, productionData.Cost);
            upgrade.Button.interactable = PlayerState.Inventory.Cash >= productionData.Cost;

            base.OnEnable();
        }

        private void UpgradeClick()
        {
            _productionService.AddProductionSize();
        }
    }
}