using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.Settings
{
    [UsedImplicitly]
    public class SettingsService : ISettingsService, IInitializable
    {
        private const string LanguageName = "Language";
        private const string GraphicsName = "Graphics";
        private const string MusicVolumeName = "MusicVolume";
        private const string AudioVolumeName = "AudioVolume";

        public LanguageType Language { get; set; }
        public GraphicsType Graphics { get; set; }

        public float AudioVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public void Initialize()
        {
            Language = (LanguageType) PlayerPrefs.GetInt(LanguageName, 0);
            Graphics = (GraphicsType) PlayerPrefs.GetInt(GraphicsName, 0);

            MusicVolume = PlayerPrefs.GetInt(MusicVolumeName, 100);
            AudioVolume = PlayerPrefs.GetInt(AudioVolumeName, 100);

            QualitySettings.SetQualityLevel((int) Graphics);
        }

        public void SetLanguage(LanguageType value)
        {
            Language = value;
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