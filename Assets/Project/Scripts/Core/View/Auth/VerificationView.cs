using Syndicate.Core.Services;
using UniRx;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class VerificationView : AuthViewBase
    {
        [Inject] private readonly IAuthService _authService;
        [Inject] private readonly IPopupService _popupService;

        [Space]
        [SerializeField] private LocalizeStringEvent verificationText;

        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private Button resendButton;

        protected override void Awake()
        {
            backButton.OnClickAsObservable().Subscribe(_ => OnBackClick()).AddTo(disposable);
            resendButton.OnClickAsObservable().Subscribe(_ => OnResendClick()).AddTo(disposable);

            ((StringVariable)verificationText.StringReference["value"]).Value = _authService.User.Email;

            base.Awake();
        }

        private void OnBackClick()
        {
            var authModel = _popupService.Get<AuthViewModel>();
            authModel.ResetViews?.Invoke();
            authModel.SignInView.SetActive(true);
        }
        
        private void OnResendClick()
        {
            _authService.EmailVerification();
        }
    }
}