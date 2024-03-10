using System;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Syndicate.Core.Sounds;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Syndicate.Core.View
{
    public class LanguageCellView : MonoBehaviour
    {
        [Inject] private readonly IAudioService _audioService;
        
        [SerializeField] private LanguageType type;
        [SerializeField] private AudioAssetId audioAssetId;

        [Space]
        [SerializeField] private Image active;

        public LanguageType Type => type;

        public Action<LanguageCellView, LanguageType> OnClickEvent { get; set; }

        private Button _button;
        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.OnClickAsObservable().Subscribe(_ => Click()).AddTo(_disposable);
        }
        
        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void Click()
        {
            _audioService.Play(audioAssetId);
            
            OnClickEvent?.Invoke(this, type);
        }
        
        public void SetActive(bool value)
        {
            active.gameObject.SetActive(value);
        }
    }
}