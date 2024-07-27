using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "RawSet", menuName = "Scriptable/Items/Raw Set", order = 1)]
    public class RawSetScriptable : ScriptableObject
    {
        [SerializeField] private List<RawItemScriptable> items;

        public List<RawItemScriptable> Items => items;
    }

    [Serializable]
    public class RawItemScriptable : ItemScriptable
    {
        public RawId Key { get => (RawId)key; set => key = value; }
    }
}