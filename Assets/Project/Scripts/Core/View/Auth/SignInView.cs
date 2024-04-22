using System.Text.RegularExpressions;
using Firebase.Auth;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class SignInView : AuthViewBase
    {
        [Inject] private readonly IAuthService _authService;
        [Inject] private readonly IPopupService _popupService;

        [Space]
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;

        [Space]
        [SerializeField] private Button signInButton;
        [SerializeField] private Button signInTestButton;
        [SerializeField] private Button signUpButton;

        private string _authCode;
        private string _email;
        private string _password;

        private bool _isError;

        protected override void Awake()
        {
            signInButton.interactable = false;

            signInButton.onClick.AddListener(SignInClick);
            signInTestButton.onClick.AddListener(SignInTestClick);
            signUpButton.onClick.AddListener(SignUpClick);
            emailField.onValueChanged.AddListener(ReadEmailField);
            passwordField.onValueChanged.AddListener(ReadPasswordField);

            base.Awake();
        }

        private async void SignInClick()
        {
            await _authService.SignIn(_email, _password);
        }
        
        private async void SignInTestClick()
        {
            await _authService.SignIn(Constants.TestEmail, Constants.TestPassword);
        }

        private void ReadEmailField(string text)
        {
            _email = text;
            var isMatch = Regex.IsMatch(text, AuthService.MatchEmailPattern);
            if (!string.IsNullOrEmpty(text) && !isMatch)
                AddError(AuthError.InvalidEmail);
            else
                RemoveError(AuthError.InvalidEmail);
        }

        private void ReadPasswordField(string text)
        {
            _password = text;
            if (text?.Length < AuthService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void SignUpClick()
        {
            if (_authService.User != null)
            {
                _authService.SignOut();
                return;
            }

            var authModel = _popupService.Get<AuthViewModel>();
            authModel.ResetViews?.Invoke();
            authModel.SignUpView.SetActive(true);
        }

        protected override void ShowError()
        {
            SetButtonState();
            base.ShowError();
        }

        private void SetButtonState()
        {
            var condition = errors.Count == 0
                            && !string.IsNullOrEmpty(_email)
                            && !string.IsNullOrEmpty(_password);

            signInButton.interactable = condition;
        }
    }
}