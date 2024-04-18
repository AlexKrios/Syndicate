using JetBrains.Annotations;
using Syndicate.Core.View;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    [UsedImplicitly]
    public class MainViewMediator : MediatorBase<MainViewModel>
    {
        [Inject] private IViewModelFactory _viewModelFactory;

        public override void Initialize()
        {
            viewModel = _viewModelFactory.Build<MainViewModel>();
            base.Initialize();
        }
    }
}