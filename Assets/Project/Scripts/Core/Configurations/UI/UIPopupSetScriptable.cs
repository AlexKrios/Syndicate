using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UIPopupSet", menuName = "Scriptable/UI/UI Popup Set", order = -92)]
    public class UIPopupSetScriptable : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour> popups;

        public List<MonoBehaviour> Popups => popups;
    }
}