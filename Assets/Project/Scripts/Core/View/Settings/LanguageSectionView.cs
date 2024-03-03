using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Localization;
using Syndicate.Core.Settings;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.View
{
    public class LanguageSectionView : MonoBehaviour
    {
        [Inject] private readonly ISettingsService _settingsController;
        [Inject] private readonly ILocalizationService _localizationService;
        
        [SerializeField] private List<LanguageCellView> languages;

        public Action OnClickEvent { get; set; }

        private LanguageCellView _activeLanguage;
        private LanguageCellView ActiveLanguage
        {
            get => _activeLanguage;
            set
            {
                if (_activeLanguage != null)
                    _activeLanguage.SetActive(false);

                _activeLanguage = value;
                _activeLanguage.SetActive(true);
            }
        }

        public void Initialize()
        {
            languages.ForEach(x => x.OnClickEvent += OnLanguageClick);

            ActiveLanguage = languages.First(x => x.Type == _settingsController.Language);
        }

        private void OnLanguageClick(LanguageCellView cell, LanguageType type)
        {
            if (ActiveLanguage == cell) return;

            _settingsController.SetLanguage(type);
            ActiveLanguage = cell;
            _localizationService.Reload();

            OnClickEvent?.Invoke();
        }
    }
}