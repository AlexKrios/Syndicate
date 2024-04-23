using Syndicate.Core.Profile;
using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Syndicate.Hub.View.Main
{
    public class ProfileView : MonoBehaviour
    {
        [Inject] private readonly IGameService _gameService;

        [SerializeField] private TMP_Text cashText;
        [SerializeField] private TMP_Text userName;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();

        private void OnEnable()
        {
            cashText.text = PlayerProfile.Inventory.Cash.ToString();
            userName.text = PlayerProfile.Profile.Name;
        }
    }
}