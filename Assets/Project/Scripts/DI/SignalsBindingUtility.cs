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

            container.DeclareSignal<ExperienceChangeSignal>().OptionalSubscriber();
            container.DeclareSignal<LevelChangeSignal>().OptionalSubscriber();

            container.DeclareSignal<CashChangeSignal>().OptionalSubscriber();
            container.DeclareSignal<DiamondChangeSignal>().OptionalSubscriber();

            container.DeclareSignal<ProductionSizeChangeSignal>();
            container.DeclareSignal<ExpeditionSizeChangeSignal>();

            container.DeclareSignal<OrdersChangeSignal>();
        }
    }
}