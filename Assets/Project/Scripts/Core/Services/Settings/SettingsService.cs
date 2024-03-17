using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class SettingsService : ISettingsService
    {
        private const string LanguageName = "Language";
        private const string GraphicsName = "Graphics";
        private const string MusicVolumeName = "MusicVolume";
        private const string AudioVolumeName = "AudioVolume";

        public LanguageType Language { get; set; }
        public GraphicsType Graphics { get; set; }

        public float AudioVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public UniTask Initialize()
        {
            Language = (LanguageType) PlayerPrefs.GetInt(LanguageName, 0);
            Graphics = (GraphicsType) PlayerPrefs.GetInt(GraphicsName, 0);

            MusicVolume = PlayerPrefs.GetInt(MusicVolumeName, 100);
            AudioVolume = PlayerPrefs.GetInt(AudioVolumeName, 100);

            QualitySettings.SetQualityLevel((int) Graphics);

            return UniTask.CompletedTask;
        }

        public void SetLanguage(LanguageType value)
        {
            Language = value;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)value];
            PlayerPrefs.SetInt(LanguageName, (int) value);
        }
        
        public void SetGraphics(GraphicsType value)
        {
            Graphics = value;
            PlayerPrefs.SetInt(GraphicsName, (int) value);
            QualitySettings.SetQualityLevel((int) value);
        }

        public void SetMusicVolume(float value)
        {
            MusicVolume = value;
            PlayerPrefs.SetInt(MusicVolumeName, (int) value);
        }
        
        public void SetAudioVolume(float value)
        {
            AudioVolume = value;
            PlayerPrefs.SetInt(AudioVolumeName, (int) value);
        }
    }
}