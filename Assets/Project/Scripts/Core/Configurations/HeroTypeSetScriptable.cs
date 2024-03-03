using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "Hero Type Set", menuName = "Scriptable/Hero Type Set", order = 0)]
    public class HeroTypeSetScriptable : ListScriptableObject<HeroTypeScriptable> { }

    [Serializable]
    public class HeroTypeScriptable
    {
        [SerializeField] private HeroTypeId heroTypeId;
    }
}