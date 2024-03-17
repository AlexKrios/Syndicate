using System;
using JetBrains.Annotations;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public interface IExperienceService
    {
        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        void SetExperience(int experience);
    }
}