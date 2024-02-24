﻿using System.Collections.Generic;
using System.Globalization;
using Syndicate.Core.Settings;
using Syndicate.Core.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class SettingsView : ViewBase<SettingsViewModel>
    {
        [Inject] private readonly ISettingsService _settingsService;
        [Inject] private readonly IMusicService _musicService;
        [Inject] private readonly IAudioService _audioService;

        /*[Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AuthService _authService;
        [Inject] private readonly SceneService _sceneService;*/

        [SerializeField] private TMP_Text title;
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
        [SerializeField] private TMP_Text graphicSliderText;

        [Space]
        [SerializeField] private LanguageSectionView language;

        private Dictionary<GraphicsType, string> _graphicQualityKeys;

        private void Awake()
        {
            _graphicQualityKeys = new Dictionary<GraphicsType, string>
            {
                [GraphicsType.Low] = "Low",
                [GraphicsType.Medium] = "Medium",
                [GraphicsType.High] = "High"
            };

            musicSlider.onValueChanged.AddListener(UpdateMusicSliderValue);
            audioSlider.onValueChanged.AddListener(UpdateAudioSliderValue);
            graphicSlider.onValueChanged.AddListener(UpdateGraphicSliderValue);

            //language.OnClickEvent += OnLanguageClick;
        }

        protected void OnDestroy()
        {
            musicSlider.onValueChanged.RemoveListener(UpdateMusicSliderValue);
            audioSlider.onValueChanged.RemoveListener(UpdateAudioSliderValue);
            graphicSlider.onValueChanged.RemoveListener(UpdateGraphicSliderValue);

            //language.OnClickEvent -= OnLanguageClick;
        }

        protected override void OnBind()
        {
            base.OnBind();

            musicSlider.value = _settingsService.MusicVolume;
            audioSlider.value = _settingsService.AudioVolume;
            UpdateGraphicSliderValue((float)_settingsService.Graphics);
            language.Initialize();

            /*if (!_authService.IsGooglePlayConnected())
                _signOut.onClick.AddListener(OnSignOutClick);
            else
                _signOut.gameObject.SetActive(false);*/

            //title.text = _localizationService.GetLanguageValue(LocalizationKeys.SettingsMenuTitleKey);
        }

        private async void OnSignOutClick()
        {
            /*_authService.SignOut();
            await _sceneService.LoadScene(SceneName.Auth);
            Close();*/
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
            graphicSliderText.text = _graphicQualityKeys[graphics];
            _settingsService.SetGraphics(graphics);
        }

        private void OnLanguageClick()
        {
            //title.text = _localizationService.GetLanguageValue(LocalizationKeys.SettingsMenuTitleKey);
        }
    }
}