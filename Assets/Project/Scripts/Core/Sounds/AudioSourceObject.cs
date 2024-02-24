using UnityEngine;

namespace Syndicate.Core.Sounds
{
    public class AudioSourceObject
    {
        private readonly AudioSource _audioSource;

        public bool IsPlaying => _audioSource.isPlaying;

        public AudioSourceObject(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}