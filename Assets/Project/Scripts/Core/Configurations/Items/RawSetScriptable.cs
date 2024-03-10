using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "RawSet", menuName = "Scriptable/Items/Raw Set", order = 1)]
    public class RawSetScriptable : ListScriptableObject<RawScriptable> { }

    [Serializable]
    public class RawScriptable : ItemScriptable
    {
        public RawId Id => (RawId)id;
    }
}