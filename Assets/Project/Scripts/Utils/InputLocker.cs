using UnityEngine;
using UnityEngine.EventSystems;

namespace Syndicate.Core.Utils
{
    public class InputLocker : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;

        public void Lock()
        {
            eventSystem.enabled = false;
        }

        public void Unlock()
        {
            eventSystem.enabled = true;
        }
    }
}