using Syndicate.Core.Entities;
using Syndicate.Core.Sounds;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject(Id = "Test")] private MusicController _musicController;

    private void Start()
    {
        _musicController.Play(new SoundAssetId("Epic"));
    }
}