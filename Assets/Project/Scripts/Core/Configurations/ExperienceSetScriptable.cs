using System;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ExperienceSet", menuName = "Scriptable/Profile/Experience Set", order = 0)]
    public class ExperienceSetScriptable : ListScriptableObject<ExperienceScriptable> { }

    [Serializable]
    public class ExperienceScriptable
    {
        [SerializeField] private int level;
        [SerializeField] private int cap;

        public int Level => level;
        public int Cap => cap;
    }
}