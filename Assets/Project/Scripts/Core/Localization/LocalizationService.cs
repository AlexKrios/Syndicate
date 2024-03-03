using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Settings;
using UnityEngine;
using Zenject;

namespace Syndicate.Core.Localization
{
    [UsedImplicitly]
    public class LocalizationService : ILocalizationService, IInitializable
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly ISettingsService _settingsService;

        private const char LineSeparator = '\n';
        private readonly string[] _fieldSeparator = { "\",\"" };

        private readonly Dictionary<string, string> _localizationDictionary = new();

        public string GetLanguageValue(string key)
        {
            return key == string.Empty ? string.Empty : _localizationDictionary[key];
        }

        public void Initialize()
        {
            if (_localizationDictionary.Count != 0)
                _localizationDictionary.Clear();

            _settings.Localization.ForEach(GetLanguageValues);
        }

        public void Reload()
        {
            Initialize();
        }

        private void GetLanguageValues(TextAsset file)
        {
            var lines = file.text.Split(LineSeparator);
            var attributeIndex = -1;
            var headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);

            for (var i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(_settingsService.Language.ToString()))
                {
                    attributeIndex = i;
                    break;
                }
            }

            var parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = parser.Split(line);

                for (var k = 0; k < fields.Length; k++)
                {
                    fields[k] = fields[k].TrimStart(' ', '"');
                    fields[k] = fields[k].Replace("\"","");
                }
                if (fields.Length > attributeIndex)
                {
                    var key = fields[0];
                    if(_localizationDictionary.ContainsKey(key))
                        continue;
                    
                    if (key.Substring(0,2) == "/-")
                        continue;

                    var value = fields[attributeIndex];
                    _localizationDictionary.Add(key, value);
                }
            }
        }

        [Serializable]
        public class Settings
        {
            public List<TextAsset> localization;
            
            public List<TextAsset> Localization => localization;
        }
    }
}