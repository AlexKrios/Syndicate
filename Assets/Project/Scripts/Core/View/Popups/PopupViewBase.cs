using DG.Tweening;
using Syndicate.Core.Utils;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupViewBase<T> : ViewBase<T> where T : ViewModelBase, IPopupViewModel
    {
        [Inject] private readonly InputLocker _inputLocker;

        [SerializeField] private CanvasGroup canvasGroup;

        private Sequence _sequence;

        protected virtual void OnEnable()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .PrependCallback(() => _inputLocker.Lock(true))
                .Append(canvasGroup.DOFade(1f, 0.25f).From(0f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    _inputLocker.Lock(false);
                });
        }

        protected virtual void Close()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .PrependCallback(() => _inputLocker.Lock(true))
                .Append(canvasGroup.DOFade(0f, 0.25f).From(1f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    _inputLocker.Lock(false);
                    ViewModel.Hide?.Invoke();
                });
        }
    }
}