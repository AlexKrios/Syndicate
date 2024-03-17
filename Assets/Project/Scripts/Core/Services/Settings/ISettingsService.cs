using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public interface ISettingsService
    {
        LanguageType Language { get; set; }
        GraphicsType Graphics { get; set; }
        
        float AudioVolume { get; }
        float MusicVolume { get; }

        UniTask Initialize();

        void SetLanguage(LanguageType value);

        void SetGraphics(GraphicsType value);

        void SetMusicVolume(float value);

        void SetAudioVolume(float value);
    }
}