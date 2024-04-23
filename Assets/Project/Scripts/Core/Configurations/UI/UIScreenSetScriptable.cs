using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UIScreenSet", menuName = "Scriptable/UI/UI Screen Set", order = -93)]
    public class UIScreenSetScriptable : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour> screens;

        public List<MonoBehaviour> Screens => screens;
    }
}