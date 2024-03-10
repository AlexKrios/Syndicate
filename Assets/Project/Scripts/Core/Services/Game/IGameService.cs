using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    public interface IGameService
    {
        void CreateGame();

        PlayerProfile GetPlayerProfile();
    }
}