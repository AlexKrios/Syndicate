using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Syndicate.Core.View
{
    public class LoadingView : PopupViewBase<LoadingViewModel>
    {
        //[SerializeField] private TMP_Text text;
        [SerializeField] private Slider slider;

        protected override void OnBind()
        {
            ViewModel.LoadingPercent.Subscribe(x =>
            {
                if (x == 0)
                    slider.value = 0;

                slider.DOValue(x * 100, Constants.LoadingStepTime);
            });
        }
    }
}