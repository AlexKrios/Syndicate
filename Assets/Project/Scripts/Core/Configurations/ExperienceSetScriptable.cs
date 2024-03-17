using System;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "ExperienceSet", menuName = "Scriptable/Experience Set", order = -20)]
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