using System;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Hub.View.Main
{
    [RequireComponent(typeof(Button))]
    public class NavigationTabView : MonoBehaviour
    {
        public Action<NavigationTabView> OnClickEvent { get; set; }

        [SerializeField] private MainViewType tabType;
        [SerializeField] private GameObject tabObject;

        private Button _button;

        public MainViewType TabType => tabType;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            Debug.LogError("Click");
            OnClickEvent?.Invoke(this);
        }

        public void SetActive()
        {
            Debug.LogError(tabObject);
            tabObject.SetActive(true);
        }

        public void SetInactive()
        {
            tabObject.SetActive(false);
        }
    }
}