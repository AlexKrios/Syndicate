using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Core.View
{
    public class LoadingView : ViewBase<LoadingViewModel>
    {
        [SerializeField] private Slider slider;

        protected override void OnBind()
        {
            ViewModel.LoadingPercent.Subscribe(x =>
            {
                slider.DOValue(x * 100, Constants.LoadingStepTime);
            });
        }
    }
}