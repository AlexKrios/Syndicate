using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UIComponentSetScriptable", menuName = "Scriptable/UI/UI Component Set", order = -91)]
    public class UIComponentSetScriptable : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour> components;

        public List<MonoBehaviour> Components => components;
    }
}