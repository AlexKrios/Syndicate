using Cysharp.Threading.Tasks;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IGameService
    {
        PlayerState GetPlayerState();

        void CreatePlayerProfile();

        UniTask LoadPlayerProfile();
    }
}