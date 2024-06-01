using DG.Tweening;
using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class MainView : ScreenViewBase<MainViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IExperienceService _experienceService;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IPopupService _popupService;

        [SerializeField] private TMP_Text profileName;
        [SerializeField] private TMP_Text profileCash;
        [SerializeField] private TMP_Text profileDiamond;
        [SerializeField] private TMP_Text levelCount;
        [SerializeField] private Slider levelSlider;

        [Space]
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button unitsButton;
        [SerializeField] private Button storageButton;
        [SerializeField] private Button battleButton;

        [Space]
        [SerializeField] private Button productionQueueUpgrade;

        private PlayerState PlayerState => _gameService.GetPlayerState();

        private void Awake()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClick);
            profileButton.onClick.AddListener(OnSettingsButtonClick);
            unitsButton.onClick.AddListener(OnUnitsButtonClick);
            storageButton.onClick.AddListener(OnStorageButtonClick);
            battleButton.onClick.AddListener(OnBattleButtonClick);
            productionQueueUpgrade.onClick.AddListener(OnProductionQueueUpgradeClick);
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ExperienceChangeSignal>(OnExperienceChange);
            _signalBus.Subscribe<LevelChangeSignal>(OnLevelChange);

            profileName.text = PlayerState.Profile.Name;
            profileCash.text = PlayerState.Inventory.Cash.ToString();
            profileDiamond.text = PlayerState.Inventory.Diamond.ToString();

            levelCount.text = _experienceService.GetCurrentLevel().ToString();
            levelSlider.value = _experienceService.GetCurrentLevelPercent(PlayerState.Profile.Experience);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ExperienceChangeSignal>(OnExperienceChange);
            _signalBus.Unsubscribe<LevelChangeSignal>(OnLevelChange);
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

        private void OnBattleButtonClick()
        {
            SceneManager.LoadScene("Battle");
        }

        private void OnProductionQueueUpgradeClick()
        {
            _popupService.Show<ProductionQueueUpgradeViewModel>();
        }

        private void OnExperienceChange(ExperienceChangeSignal signal)
        {
            var currentPercent = _experienceService.GetCurrentLevelPercent(PlayerState.Profile.Experience);
            levelSlider.DOValue(currentPercent, 0.5f);
        }

        private void OnLevelChange(LevelChangeSignal signal)
        {
            levelCount.text = signal.Level.ToString();
        }
    }
}