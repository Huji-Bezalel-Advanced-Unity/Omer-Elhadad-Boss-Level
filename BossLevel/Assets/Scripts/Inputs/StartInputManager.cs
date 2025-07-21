using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class StartInputManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(FadeOutAudio(1f)); // Fade out over 1 second
            SceneTransitionManager.Instance.ChangeScene("SampleScene");
        }
    }

    private IEnumerator FadeOutAudio(float duration)
    {
        float startVolume = _audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = 0f;
        _audioSource.Stop();
    }
}