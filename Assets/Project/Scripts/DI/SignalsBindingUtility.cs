using Syndicate.Battle;
using Syndicate.Core.Signals;
using Zenject;

namespace Syndicate.DI
{
    public static class SignalsBindingUtility
    {
        public static void DeclareSignals(this DiContainer container)
        {
            container.DeclareSignal<SignInSignal>();
            container.DeclareSignal<SignUpSignal>();
            container.DeclareSignal<VerificationSignal>();

            container.DeclareSignal<ProductionChangeSignal>();
        }
    }
}