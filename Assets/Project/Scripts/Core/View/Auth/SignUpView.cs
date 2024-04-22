using System.Text.RegularExpressions;
using Firebase.Auth;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class SignUpView : AuthViewBase
    {
        [Inject] private readonly IAuthService _authService;
        [Inject] private readonly IPopupService _popupService;

        [Space]
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private TMP_InputField confirmField;

        [Space]
        [SerializeField] private Button signUpButton;
        [SerializeField] private Button googlePlayButton;
        [SerializeField] private Button backButton;

        private string _authCode;
        private string _email;
        private string _password;
        private string _confirm;

        protected override void Awake()
        {
            signUpButton.interactable = false;

            signUpButton.onClick.AddListener(SignUpClick);
            googlePlayButton.onClick.AddListener(GooglePlayClick);
            backButton.onClick.AddListener(BackClick);
            emailField.onValueChanged.AddListener(ReadEmailField);
            passwordField.onValueChanged.AddListener(ReadFirstPasswordField);
            confirmField.onValueChanged.AddListener(ReadSecondPasswordField);
            
            base.Awake();
        }

        private async void SignUpClick()
        {
            await _authService.SignUp(_email, _password);
        }
        
        private void GooglePlayClick()
        {
            //_authService.GooglePlaySignManually();
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

        private void ReadFirstPasswordField(string text)
        {
            _password = text;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(_confirm) && text != _confirm)
                AddError(AuthError.MissingPassword);
            else
                RemoveError(AuthError.MissingPassword);
            
            if (text?.Length < AuthService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void ReadSecondPasswordField(string text)
        {
            _confirm = text;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(_password) && text != _password)
                AddError(AuthError.MissingPassword);
            else
                RemoveError(AuthError.MissingPassword);
            
            if (text?.Length < AuthService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void BackClick()
        {
            var authModel = _popupService.Get<AuthViewModel>();
            authModel.ResetViews?.Invoke();
            authModel.SignInView.SetActive(true);
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
                            && !string.IsNullOrEmpty(_password)
                            && !string.IsNullOrEmpty(_confirm);
            
            signUpButton.interactable = condition;
        }
    }
}