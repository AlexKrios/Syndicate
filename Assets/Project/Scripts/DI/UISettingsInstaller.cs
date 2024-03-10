using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    [CreateAssetMenu(fileName = "UISettingsInstaller", menuName = "Scriptable/DI/UI Settings")]
    public class UISettingsInstaller : ScriptableObjectInstaller<UISettingsInstaller>
    {
        [SerializeField] private PopupService.Settings popups;
        [SerializeField] private ProductionSectionFactory.Settings production;

        public override void InstallBindings()
        {
            Container.BindInstance(popups);
            Container.BindInstance(production);
        }
    }
}