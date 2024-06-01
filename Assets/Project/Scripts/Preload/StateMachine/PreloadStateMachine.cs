using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Syndicate.Core.View;
using UnityEngine.SceneManagement;
using Zenject;

namespace Syndicate.Preload.StateMachine
{
    [UsedImplicitly]
    public class PreloadStateMachine : AbstractStateMachine
    {
        [Inject] private readonly IPopupService _popupService;
        [Inject] private readonly GameInitializeState.Factory _gameInitializeStateFactory;
        [Inject] private readonly ServiceInitializeState.Factory _serviceInitializeStateFactory;
        [Inject] private readonly AuthInitializeState.Factory _authInitializeStateFactory;
        [Inject] private readonly ProfileInitializeState.Factory _profileInitializeStateFactory;

        private LoadingViewModel _loadingModel;

        public void Initialize()
        {
            AddState<GameInitializeState>(_gameInitializeStateFactory.Create());
            AddState<ServiceInitializeState>(_serviceInitializeStateFactory.Create());
            AddState<AuthInitializeState>(_authInitializeStateFactory.Create());
            AddState<ProfileInitializeState>(_profileInitializeStateFactory.Create());

            _loadingModel = _popupService.Show<LoadingViewModel>();
            SetLoadingPercent(0);
        }

        public void SetLoadingPercent(float progress)
        {
            _loadingModel ??= _popupService.Get<LoadingViewModel>();
            _loadingModel.LoadingPercent.Value = progress;
        }

        public async UniTask SetLoadingFinish()
        {
            SetLoadingPercent(100);
            await UniTask.Delay(1000);

            SceneManager.LoadScene("Hub");
            SetLoadingPercent(0);
            _loadingModel.Hide?.Invoke();
        }
    }
}