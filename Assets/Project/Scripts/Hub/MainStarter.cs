using Syndicate.Core.View;
using Syndicate.Hub.View.Main;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Hub
{
    public class MainStarter : MonoBehaviour
    {
        [Inject] private IScreenService _screenService;

        private void Awake()
        {
            _screenService.Show<MainViewModel>();
        }
    }
}