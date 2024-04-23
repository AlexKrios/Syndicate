using Syndicate.Battle;
using Zenject;

namespace Syndicate.DI
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BattleManager>().AsSingle();
        }
    }
}