using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Signals;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class AuthService : IAuthService
    {
        //private const string FirebaseProvider = "firebase";
        private const string PasswordProvider = "password";
        private const string GooglePlayProvider = "playgames.google.com";
        
        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        public const float MinPasswordLength = 6;

        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IAssetsService _assetsService;

        public IReactiveProperty<AuthStatus> Status { get; } = new ReactiveProperty<AuthStatus>(AuthStatus.None);
        public IReactiveProperty<AuthError> ErrorCode { get; } = new ReactiveProperty<AuthError>(AuthError.None);

        private readonly Dictionary<AuthError, string> _errorsList;
        public FirebaseUser User { get; private set; }

        private static FirebaseAuth Auth => FirebaseAuth.DefaultInstance;

        public AuthService()
        {
            _errorsList = new Dictionary<AuthError, string>
            {
                { AuthError.EmailAlreadyInUse, "auth_email_already_in_use" },
                { AuthError.InvalidEmail, "auth_invalid_email" },
                { AuthError.TooManyRequests, "auth_too_many_requests" },
                { AuthError.WeakPassword, "auth_weak_password" },
                { AuthError.MissingPassword, "auth_missing_password" },
                { AuthError.UserNotFound, "auth_user_not_found" },
                { AuthError.UnverifiedEmail, "auth_unverified_email" }
            };
        }

        public LocalizedString GetErrorLocalizationKey(AuthError error)
        {
            var key = _errorsList[error];
            return _assetsService.GetLocalize(new LocalizeAssetId(key));
        }

        public void Initialize()
        {
            Auth.StateChanged += FirebaseAuthStateChanged;
            FirebaseAuthStateChanged(this, null);

            if (User is { IsEmailVerified: true }/* && !_user.Email.Contains(Constants.TestEmail)*/)
            {
                if (IsProviderUsed(PasswordProvider))
                {
                    Status.Value = AuthStatus.Success;
                }
                else if (IsProviderUsed(GooglePlayProvider))
                {
                    //InitializeGooglePlay();
                }
            }
            else
            {
                Status.Value = AuthStatus.Failure;
            }
        }

        private void FirebaseAuthStateChanged(object sender, EventArgs eventArgs) 
        {
            User = Auth.CurrentUser;
        }
        
        private bool IsProviderUsed(string providerKey)
        {
            foreach (var provider in User.ProviderData)
            {
                if (provider.ProviderId.Equals(providerKey))
                    return true;
            }
            
            return false;
        }

        public async UniTask SignIn(string email, string password)
        {
            var task = Auth.SignInWithEmailAndPasswordAsync(email, password);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogError((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }

            if (!User.IsEmailVerified/* && !_user.Email.Contains(Constants.TestEmail)*/)
            {
                await EmailVerification();
                return;
            }

            Status.Value = AuthStatus.Success;
        }

        public async UniTask SignUp(string email, string password)
        {
            var task = Auth.CreateUserWithEmailAndPasswordAsync(email, password);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogError((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }

            await EmailVerification();
        }

        public async UniTask EmailVerification()
        {
            var task = User.SendEmailVerificationAsync();
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogError((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }

            _signalBus.Fire(new VerificationSignal());
        }

        public void SignOut()
        {
            Status.Value = AuthStatus.None;
            Auth.SignOut();
        }

        /*#region Google Play

        public bool IsGooglePlayConnected()
        {
            return IsProviderUsed(GooglePlayProvider);
        }
        
        private void InitializeGooglePlay()
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(InitializeGooglePlayCallback);
        }

        private async void InitializeGooglePlayCallback(SignInStatus status)
        {
            switch (status)
            {
                case SignInStatus.Success:
                    await UniTask.RunOnThreadPool(GooglePlaySignIn);
                    break;

                case SignInStatus.Canceled:
                    Auth.SignOut();
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    Debug.LogWarning("Cancel Google Play Sign In");
                    break;

                case SignInStatus.InternalError:
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    Debug.LogWarning("Internal Error Google Play Sign In");
                    break;

                default:
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    break;
            }
        }
        
        public void GooglePlaySignIn()
        {
            PlayGamesPlatform.Instance.RequestServerSideAccess(true, SignInWithCredential);
        }
        
        public void GooglePlaySignManually()
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(_ =>
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, SignInWithCredential);
            });
        }

        private async void SignInWithCredential(string authCode)
        {
            var credential = PlayGamesAuthProvider.GetCredential(authCode);
            var task = Auth.SignInWithCredentialAsync(credential);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
            }

            await SignInSuccess();
            Debug.LogWarning("Success Google Play Sign In");
        }

        #endregion*/
    }
}