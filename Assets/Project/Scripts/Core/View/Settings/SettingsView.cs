using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Syndicate.Core.Services;
using Syndicate.Core.Sounds;
using Syndicate.Hub.View.Main;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class SettingsView : PopupViewBase<SettingsViewModel>
    {
        [Inject] private readonly IAuthService _authService;
        [Inject] private readonly ISettingsService _settingsService;
        [Inject] private readonly IMusicService _musicService;
        [Inject] private readonly IAudioService _audioService;
        [Inject] private readonly IScreenService _screenService;

        [SerializeField] private Button signOut;
        [SerializeField] private Button close;

        [Header("Music Slider Components")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private TMP_Text musicSliderText;

        [Header("Audio Slider Components")]
        [SerializeField] private Slider audioSlider;
        [SerializeField] private TMP_Text audioSliderText;

        [Header("Graphic Slider Components")]
        [SerializeField] private Slider graphicSlider;
        [SerializeField] private LocalizeStringEvent graphicSliderText;
        [SerializeField] private List<LocalizedString> graphicTextKeys;

        [Space]
        [SerializeField] private List<LanguageCellView> languages;

        private LanguageCellView _activeLanguage;
        private LanguageCellView ActiveLanguage
        {
            get => _activeLanguage;
            set
            {
                if (_activeLanguage != null)
                    _activeLanguage.SetActive(false);

                _activeLanguage = value;
                _activeLanguage.SetActive(true);
            }
        }

        private void Awake()
        {
            musicSlider.onValueChanged.AddListener(UpdateMusicSliderValue);
            audioSlider.onValueChanged.AddListener(UpdateAudioSliderValue);
            graphicSlider.onValueChanged.AddListener(UpdateGraphicSliderValue);

            languages.ForEach(x => x.OnClickEvent += OnLanguageClick);
            ActiveLanguage = languages.First(x => x.Type == _settingsService.Language);
        }

        protected override void OnBind()
        {
            base.OnBind();

            ViewModel.GameObject = gameObject;

            close.onClick.AddListener(() => ViewModel.Hide?.Invoke());

            musicSlider.value = _settingsService.MusicVolume;
            audioSlider.value = _settingsService.AudioVolume;
            UpdateGraphicSliderValue((float)_settingsService.Graphics);

            signOut.onClick.AddListener(OnSignOutClick);
            /*if (!_authService.IsGooglePlayConnected())
                _signOut.onClick.AddListener(OnSignOutClick);
            else
                _signOut.gameObject.SetActive(false);*/
        }

        private void OnSignOutClick()
        {
            _authService.SignOut();
            ViewModel.Hide?.Invoke();
            _screenService.Get<MainViewModel>(true).Hide?.Invoke();

            SceneManager.LoadScene("Preloader");
        }

        private void UpdateMusicSliderValue(float value)
        {
            musicSliderText.text = value.ToString(CultureInfo.CurrentCulture);
            _musicService.SetVolume(value);
        }

        private void UpdateAudioSliderValue(float value)
        {
            audioSliderText.text = value.ToString(CultureInfo.CurrentCulture);
            _audioService.SetVolume(value);
        }

        private void UpdateGraphicSliderValue(float value)
        {
            var graphics = (GraphicsType) value;
            graphicSliderText.StringReference = graphicTextKeys[(int)value];
            _settingsService.SetGraphics(graphics);
        }

        private void OnLanguageClick(LanguageCellView cell, LanguageType type)
        {
            if (ActiveLanguage == cell) return;

            _settingsService.SetLanguage(type);
            ActiveLanguage = cell;
        }
    }
}