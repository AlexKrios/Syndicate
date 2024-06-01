using Syndicate.Core.Services;
using Syndicate.Core.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class RequestButton : MonoBehaviour
    {
        [Inject] private InputLocker _inputLocker;
        [Inject] private IApiService _apiService;

        [SerializeField] private Button button;

        public Button Button => button;

        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _apiService.IsRequestInProgress.Subscribe(x => _inputLocker.Lock(x)).AddTo(_disposable);
        }
    }
}