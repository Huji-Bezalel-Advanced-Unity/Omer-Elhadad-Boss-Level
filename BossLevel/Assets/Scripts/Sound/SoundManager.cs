using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip buttonPressedSound;
    [SerializeField] private AudioClip gameBeginSound;
    
    private AudioSource _soundEffectsSource;
    private AudioSource _backgroundMusicSource;
    
    protected override void Awake()
    {
        base.Awake();
        _backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        PlayBackgroundMusic(backgroundMusic);
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        _soundEffectsSource = gameObject.AddComponent<AudioSource>();
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        _backgroundMusicSource.clip = clip;
        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.Play();
    }
    
    public void OnButtonPressed()
    {
        PlaySoundEffect(buttonPressedSound);
    }
    
    public void ChangeBackgroundMusicVolume(float volume)
    {
        _backgroundMusicSource.volume = volume;
    }
    public void ChangeSoundEffectsVolume(float volume)
    {
        _soundEffectsSource.volume = volume;
    }
    public void ChangeAllSoundsVolume(float volume)
    {
        _backgroundMusicSource.volume = volume;
        _soundEffectsSource.volume = volume;
    }
    
    public void PlaySoundEffect(AudioClip clip, float pitch = 1.0f)
    {
        if (!_soundEffectsSource || !clip) return;
        _soundEffectsSource.pitch = pitch;
        _soundEffectsSource.PlayOneShot(clip);
    }
}