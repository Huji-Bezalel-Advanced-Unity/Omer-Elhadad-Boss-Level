using UnityEngine;

public class SettingsSoundSliders : MonoBehaviour
{
    private float _masterVolume = 1f; // Default master volume

    public void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
        SoundManager.Instance.ChangeAllSoundsVolume(_masterVolume);
    }

    public void ChangeBackgroundMusicVolume(float volume)
    {
        float scaledVolume = volume * _masterVolume;
        SoundManager.Instance.ChangeBackgroundMusicVolume(scaledVolume);
    }

    public void ChangeSoundEffectsVolume(float volume)
    {
        float scaledVolume = volume * _masterVolume;
        SoundManager.Instance.ChangeSoundEffectsVolume(scaledVolume);
    }
}