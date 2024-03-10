using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellReadyState : IState
    {
        public ProductionCellReadyState(ProductionQueueCellView cell) { }

        public void Enter() { }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellReadyState> { }
    }
}