using JetBrains.Annotations;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public interface IExperienceService
    {
        int GetCurrentLevel();
        float GetCurrentLevelPercent(int experience);

        void SetExperience(int experience);
    }
}