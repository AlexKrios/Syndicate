using Newtonsoft.Json;

namespace Syndicate.Core.Profile
{
    public class ExperienceState
    {
        [JsonProperty("Experience")]
        public int experience;

        [JsonProperty("Level")]
        public int level;
    }
}