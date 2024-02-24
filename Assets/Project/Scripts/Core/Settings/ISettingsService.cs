using JetBrains.Annotations;

namespace Syndicate.Core.Settings
{
    [UsedImplicitly]
    public interface ISettingsService
    {
        LanguageType Language { get; set; }
        GraphicsType Graphics { get; set; }
        
        float AudioVolume { get; }
        float MusicVolume { get; }

        void SetLanguage(LanguageType value);

        void SetGraphics(GraphicsType value);

        void SetMusicVolume(float value);

        void SetAudioVolume(float value);
    }
}