using System.Collections.Generic;
using System.Linq;
using Firebase.Auth;
using Syndicate.Core.Services;
using UniRx;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public abstract class AuthViewBase : MonoBehaviour
    {
        [Inject] private readonly IAuthService _authService;

        [SerializeField] private GameObject errorWrapper;
        [SerializeField] private LocalizeStringEvent errorText;

        protected readonly HashSet<AuthError> errors = new();
        protected readonly CompositeDisposable disposable = new();

        protected virtual void Awake()
        {
            errorWrapper.SetActive(false);

            _authService.ErrorCode.Subscribe(x => AddError(x, true)).AddTo(disposable);

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }

        private void OnDestroy()
        {
            disposable.Dispose();
        }

        protected virtual void ShowError()
        {
            if (errors.Count == 0)
            {
                errorWrapper.SetActive(false);
                return;
            }
            
            errorWrapper.SetActive(true);
            errorText.StringReference = _authService.GetErrorLocalizationKey(errors.First());
        }

        protected void AddError(AuthError error, bool isError = false)
        {
            if (error == AuthError.None) return;
            
            errors.Add(error);
            ShowError();

            if (isError)
                errors.Remove(error);
        }

        protected void RemoveError(AuthError error)
        {
            errors.Remove(error);
            ShowError();
        }
    }
}