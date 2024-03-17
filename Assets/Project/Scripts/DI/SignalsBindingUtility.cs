using Syndicate.Core.Signals;
using Zenject;

namespace Syndicate.DI
{
    public static class SignalsBindingUtility
    {
        public static void DeclareSignals(this DiContainer container)
        {
            container.DeclareSignal<ProductionChangeSignal>();
        }
    }
}