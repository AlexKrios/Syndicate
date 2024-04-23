using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Hub
{
    public class HubStarter : MonoBehaviour
    {
        [Inject] private IScreenService _screenService;

        private void Awake()
        {
            _screenService.Show<MainViewModel>();
        }
    }
}