using TMPro;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationText : MonoBehaviour
    {
        [Inject] private readonly ILocalizationService _localizationService;

        [SerializeField] private string key;

        private TMP_Text _textComponent;

        private void Start()
        {
            _textComponent = GetComponent<TMP_Text>();
            _textComponent.text = _localizationService.GetLanguageValue(key);
        }
    }
}