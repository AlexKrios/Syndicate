using Syndicate.Core.View;
using UnityEngine;
using Zenject;

namespace Syndicate.DI
{
    [CreateAssetMenu(fileName = "UISettingsInstaller", menuName = "Scriptable/DI/UI Settings")]
    public class UISettingsInstaller : ScriptableObjectInstaller<UISettingsInstaller>
    {
        [SerializeField] private PopupService.Settings popups;

        public override void InstallBindings()
        {
            Container.BindInstance(popups);
        }
    }
}