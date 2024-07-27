using Cysharp.Threading.Tasks;
using DG.Tweening;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class MainView : ScreenViewBase<MainViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IPopupService _popupService;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IExperienceService _experienceService;

        [SerializeField] private TMP_Text profileName;
        [SerializeField] private TMP_Text profileCash;
        [SerializeField] private TMP_Text profileDiamond;
        [SerializeField] private TMP_Text levelCount;
        [SerializeField] private Slider levelSlider;

        [Space]
        [SerializeField] private ProductionQueueSectionView productionQueue;
        [SerializeField] private ExpeditionQueueSectionView expeditionQueue;

        [Space]
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button unitsButton;
        [SerializeField] private Button storageButton;
        [SerializeField] private Button ordersButton;

        [Space]
        [SerializeField] private Button productionQueueUpgrade;
        [SerializeField] private Button expeditionQueueUpgrade;

        private void Awake()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClick);
            profileButton.onClick.AddListener(OnSettingsButtonClick);
            unitsButton.onClick.AddListener(OnUnitsButtonClick);
            storageButton.onClick.AddListener(OnStorageButtonClick);
            ordersButton.onClick.AddListener(OnOrdersButtonClick);
            productionQueueUpgrade.onClick.AddListener(OnProductionQueueUpgradeClick);
            expeditionQueueUpgrade.onClick.AddListener(OnExpeditionQueueUpgradeClick);
        }

        private async void OnEnable()
        {
            _signalBus.Subscribe<ExperienceChangeSignal>(OnExperienceChange);
            _signalBus.Subscribe<LevelChangeSignal>(OnLevelChange);
            _signalBus.Subscribe<CashChangeSignal>(OnCashChange);
            _signalBus.Subscribe<DiamondChangeSignal>(OnDiamondChange);

            await UniTask.Yield();

            profileName.text = _gameService.Name;
            OnCashChange();
            OnDiamondChange();

            levelCount.text = _experienceService.GetCurrentLevel().ToString();
            levelSlider.value = _experienceService.GetCurrentLevelPercent();

            productionQueue.RefreshQueue();
            expeditionQueue.RefreshQueue();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ExperienceChangeSignal>(OnExperienceChange);
            _signalBus.Unsubscribe<LevelChangeSignal>(OnLevelChange);
            _signalBus.Unsubscribe<CashChangeSignal>(OnCashChange);
            _signalBus.Unsubscribe<DiamondChangeSignal>(OnDiamondChange);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _experienceService.SetExperience(10);
            }
        }

        private void OnSettingsButtonClick()
        {
            _popupService.Show<SettingsViewModel>();
        }

        private void OnUnitsButtonClick()
        {
            _screenService.Show<UnitsViewModel>();
        }

        private void OnStorageButtonClick()
        {
            _screenService.Show<StorageViewModel>();
        }

        private void OnOrdersButtonClick()
        {
            _popupService.Show<OrdersViewModel>();
        }

        private void OnProductionQueueUpgradeClick()
        {
            _popupService.Show<ProductionQueueUpgradeViewModel>();
        }

        private void OnExpeditionQueueUpgradeClick()
        {
            _popupService.Show<ExpeditionQueueUpgradeViewModel>();
        }

        private void OnExperienceChange(ExperienceChangeSignal signal)
        {
            var currentPercent = _experienceService.GetCurrentLevelPercent();
            levelSlider.DOValue(currentPercent, 0.5f);
        }

        private void OnLevelChange(LevelChangeSignal signal)
        {
            levelCount.text = signal.Level.ToString();
        }

        private void OnCashChange()
        {
            profileCash.text = _gameService.Cash.ToString();
        }

        private void OnDiamondChange()
        {
            profileDiamond.text = _gameService.Diamond.ToString();
        }
    }
}