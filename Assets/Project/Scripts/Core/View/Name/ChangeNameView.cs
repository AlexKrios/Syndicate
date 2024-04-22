using Syndicate.Core.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class ChangeNameView : ViewBase<ChangeNameViewModel>
    {
        [Inject] private readonly IApiService _apiService;

        [Space]
        [SerializeField] private TMP_InputField nameField;

        [Space]
        [SerializeField] private Button confirmButton;

        private string _name;

        private void Awake()
        {
            confirmButton.interactable = false;

            nameField.onValueChanged.AddListener(ReadNameField);
            confirmButton.onClick.AddListener(ConfirmButtonClick);
        }

        private async void ConfirmButtonClick()
        {
            await _apiService.SetPlayerName(_name);

            ViewModel.Hide?.Invoke();
            SceneManager.LoadScene("Hub");
        }

        private void ReadNameField(string text)
        {
            _name = text;
            SetButtonState();
        }

        private void SetButtonState()
        {
            var condition = !string.IsNullOrEmpty(_name);

            confirmButton.interactable = condition;
        }
    }
}