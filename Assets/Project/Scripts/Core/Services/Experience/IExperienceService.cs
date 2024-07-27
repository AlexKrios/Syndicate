using JetBrains.Annotations;
using Syndicate.Core.Profile;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public interface IExperienceService
    {
        int Experience { get; }

        void LoadData(ProfileState state);

        int GetCurrentLevel();
        float GetCurrentLevelPercent();

        void SetExperience(int experience);
    }
}