using Cysharp.Threading.Tasks;
using Firebase.Auth;
using UniRx;
using UnityEngine.Localization;

namespace Syndicate.Core.Services
{
    public interface IAuthService
    {
        FirebaseUser User { get; }

        IReactiveProperty<AuthStatus> Status { get; }
        IReactiveProperty<AuthError> ErrorCode { get; }

        void Initialize();

        LocalizedString GetErrorLocalizationKey(AuthError error);

        UniTask SignIn(string email, string password);
        UniTask SignUp(string email, string password);
        UniTask EmailVerification();

        void SignOut();
    }
}