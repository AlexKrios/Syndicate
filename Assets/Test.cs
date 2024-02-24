using Syndicate.Core.Entities;
using Syndicate.Core.Sounds;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private IMusicService _musicService;

    private void Start()
    {
        _musicService.Play(new MusicAssetId("Epic"));
    }
}