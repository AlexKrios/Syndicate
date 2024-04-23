using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using Syndicate.Core.View;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class MainView : ViewBase<MainViewModel>
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IScreenService _screenService;
        [Inject] private readonly IPopupService _popupService;

        [SerializeField] private TMP_Text profileName;

        [Space]
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private Button unitsButton;
        [SerializeField] private Button storageButton;
        [SerializeField] private Button battleButton;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();

        private void Awake()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClick);
            profileButton.onClick.AddListener(OnSettingsButtonClick);
            unitsButton.onClick.AddListener(OnUnitsButtonClick);
            storageButton.onClick.AddListener(OnStorageButtonClick);
            battleButton.onClick.AddListener(OnBattleButtonClick);

            profileName.text = PlayerProfile.Profile.Name;
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
    }
}