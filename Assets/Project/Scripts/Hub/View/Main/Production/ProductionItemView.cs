using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Hub.View.Main
{
    [RequireComponent(typeof(Button))]
    public class ProductionItemView : MonoBehaviour
    {
        public Action<ProductionItemView> OnClickEvent { get; set; }

        [SerializeField] private Transform activeBorder;

        private Button _button;
        private Sequence _sequence;

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
            _sequence = DOTween.Sequence()
                .PrependCallback(() => activeBorder.gameObject.SetActive(true))
                .Append(activeBorder.DOLocalRotate(new Vector3(0, 0, 360), 5).From(Vector3.zero).SetEase(Ease.Linear))
                .SetRelative(true)
                .SetLoops(-1);
        }

        public void SetInactive()
        {
            activeBorder.gameObject.SetActive(false);
            _sequence?.Kill();
            _sequence = null;
        }
    }
}