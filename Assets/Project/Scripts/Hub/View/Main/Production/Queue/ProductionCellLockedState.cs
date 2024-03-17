using JetBrains.Annotations;
using Syndicate.Core.StateMachine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProductionCellLockedState : IState
    {
        private readonly ProductionQueueCellView _cell;

        public ProductionCellLockedState(ProductionQueueCellView cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            _cell.SetUnlockData(10, 10);
        }

        public void Click() { }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionQueueCellView, ProductionCellLockedState> { }
    }
}