using Syndicate.Core.Services;
using Syndicate.Core.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    public class AuthView : ScreenViewBase<AuthViewModel>
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IAuthService _authService;

        [SerializeField] private SignInView signIn;
        [SerializeField] private SignUpView signUp;
        [SerializeField] private VerificationView verification;

        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _signalBus.Subscribe<VerificationSignal>(Verification);
            _authService.Status.Subscribe(x =>
            {
                switch (x)
                {
                    case AuthStatus.Success:
                        HideViews();
                        break;
                    case AuthStatus.Failure:
                        signIn.gameObject.SetActive(true);
                        break;
                }
            }).AddTo(_disposable);
        }

        protected override void OnBind()
        {
            ViewModel.ResetViews += HideViews;
            ViewModel.SignInView = signIn.gameObject;
            ViewModel.SignUpView = signUp.gameObject;
            ViewModel.VerificationView = verification.gameObject;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<VerificationSignal>(Verification);
            _disposable.Dispose();
        }

        private void HideViews()
        {
            signIn.gameObject.SetActive(false);
            signUp.gameObject.SetActive(false);
            verification.gameObject.SetActive(false);
        }

        private void Verification()
        {
            HideViews();
            verification.gameObject.SetActive(true);
        }
    }
}