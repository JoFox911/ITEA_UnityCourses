using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [SerializeField] private 
    List<SfxData> _sfxDataList = new List<SfxData>();
    [SerializeField] private 
    List<MusicData> _musicDataList = new List<MusicData>();

    [SerializeField] 
    private AudioSource _musicSource;
    [SerializeField] 
    private AudioSource _sfxSource;

    private void Awake()
    {
        _instance = this;
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
}


public enum SFXType
{
    BallAndPlatformCollision,
    BallAndBrickCollision,
    LevelStart
}

[Serializable]
public class SfxData
{
    public SFXType Type;
    public AudioClip Clip;
}

public enum MusicType
{
    Background,
    InMainMenu
}

[Serializable]
public class MusicData
{
    public MusicType Type;
    public AudioClip Music;
}