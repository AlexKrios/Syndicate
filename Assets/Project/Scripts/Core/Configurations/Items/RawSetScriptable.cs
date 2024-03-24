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
        [SerializeField] private List<RawGroupScriptable> groups;

        public List<RawItemScriptable> Items => items;
        public List<RawGroupScriptable> Groups => groups;
    }

    [Serializable]
    public class RawItemScriptable : ItemScriptable
    {
        public RawItemId Key { get => (RawItemId)key; set => key = value; }
    }

    [Serializable]
    public class RawGroupScriptable
    {
        [SerializeField] private string name;
        [SerializeField] private string key;

        public string Name { get => name; set => name = value; }
        public RawGroupId Key { get => (RawGroupId)key; set => key = value; }
    }
}