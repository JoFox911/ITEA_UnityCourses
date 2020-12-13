using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [SerializeField] 
    private AudioMixer _mixer;

    [SerializeField] private 
    List<SfxData> _sfxDataList = new List<SfxData>();
    [SerializeField] private 
    List<MusicData> _musicDataList = new List<MusicData>();

    [SerializeField] 
    private AudioSource _musicSource;
    [SerializeField] 
    private AudioSource _sfxSource;

    private const string MasterVolumeParameter = "MasterVolume";
    private const string MusicVolumeParameter = "MusicVolume";
    private const string SFXVolumeParameter = "SFXVolume";

    void Awake()
    {
        _instance = this;
        GameEvents.OnMasterVolumeChanged += ApplyMasterVolume;
        GameEvents.OnMusicVolumeChanged += ApplyMusicVolume;
        GameEvents.OnSFXVolumeChanged += ApplySFXVolume;
    }

    void OnDestroy()
    {
        GameEvents.OnMasterVolumeChanged -= ApplyMasterVolume;
        GameEvents.OnMusicVolumeChanged -= ApplyMusicVolume;
        GameEvents.OnSFXVolumeChanged -= ApplySFXVolume;
    }

    void Start()
    {
        ApplyMasterVolume();
        ApplyMusicVolume();
        ApplySFXVolume();
    }

    private void ApplyMasterVolume()
    {
        var savedMasterVolume = PlayerPrefs.GetFloat(MasterVolumeParameter, 0f);
        _mixer.SetFloat(MasterVolumeParameter, savedMasterVolume);
    }

    private void ApplyMusicVolume()
    {
        var savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeParameter, 0f);
        _mixer.SetFloat(MusicVolumeParameter, savedMusicVolume);
    }

    private void ApplySFXVolume()
    {
        var savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeParameter, 0f);
        _mixer.SetFloat(SFXVolumeParameter, savedSFXVolume);
    }

    private void StopMusicInner()
    {
        if (_musicSource != null)
        { 
            _musicSource.Stop();
        }
    }

    private void PlayMusicInner(MusicType type)
    {
        var musicData = _musicDataList.Find(music => music.Type == type);
        _musicSource.clip = musicData.Music;
        _musicSource.Play();
    }

    private void PlaySFXInner(SFXType type)
    {
        var sfxData = _sfxDataList.Find(sound => sound.Type == type);
        _sfxSource.PlayOneShot(sfxData.Clip);
    }

    public static void PlayMusic(MusicType type)
    {
        _instance.PlayMusicInner(type);
    }

    public static void PlaySFX(SFXType type)
    {
        _instance.PlaySFXInner(type);
    }

    public static void StopMusic()
    {
        _instance.StopMusicInner();
    }
}


public enum SFXType
{
    BallAndPlatformCollision,
    BallAndBrickCollision,
    BallAnImmortaldBrickCollision,
    LevelStart,
    GameOver,
    Bullet,
    BallWasted,
    EnemyKilled
}

[Serializable]
public class SfxData
{
    public SFXType Type;
    public AudioClip Clip;
}

public enum MusicType
{
    InMenu,
    Pause,
    Victory
}

[Serializable]
public class MusicData
{
    public MusicType Type;
    public AudioClip Music;
}