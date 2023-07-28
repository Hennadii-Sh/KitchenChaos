using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    [field: SerializeField]
    public float Volume { get; set; } = 0.4f;

    private AudioSource _audioSource;
    protected override void Awake()
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();

        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .5f);
        _audioSource.volume = Volume;
    }


    public void ChangeVolume()
    {
        Volume += .1f;
        Volume = Volume > 1.01f ? 0f : Volume;

        _audioSource.volume = Volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, Volume);
        PlayerPrefs.Save();
    }
}
