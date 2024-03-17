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

        [Space]
        [SerializeField] private Image tabImage;

        [Space]
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

        private Button _button;

        public MainViewType TabType => tabType;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            OnClickEvent?.Invoke(this);
        }

        public void SetActive()
        {
            tabImage.color = activeColor;
            tabObject.SetActive(true);
        }

        public void SetInactive()
        {
            tabImage.color = inactiveColor;
            tabObject.SetActive(false);
        }
    }
}