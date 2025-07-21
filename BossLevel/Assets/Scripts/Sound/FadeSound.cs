using System;
using UnityEngine;

public class FadeSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration = 1f;
    
    private bool _gameOverFlag;

    private void OnEnable()
    {
        _gameOverFlag = false;
        PlayerHealthEvents.PlayerDeathEvent += FadeOutOnWorldStop;
        HealthEventManager.BossDeathEvent += FadeOutOnWorldStop;
    }

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeInAudio());
        }
    }

    private System.Collections.IEnumerator FadeInAudio()
    {
        float startVolume = 0f;
        float time = 0f;

        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void FadeOutOnWorldStop()
    {
        if (_gameOverFlag)
            return;
        _gameOverFlag = true;
        if (audioSource != null)
            StartCoroutine(FadeOutAudio());
    }

    private System.Collections.IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}