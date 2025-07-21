using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private RectTransform settingsPanelRect;
    [SerializeField] private float bottomPosY, middlePosY;
    [SerializeField] private float duration = 0.5f;

    private const string MainMenuSceneName = "MainMenu";
    private bool _isTransitioning;

    private void Update()
    {
        if (_isTransitioning) return;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (background.activeSelf)
        {
            OnResume();
        }
        else
        {
            OnPause();
        }
    }

    public void OnPause()
    {
        _isTransitioning = true;
        background.SetActive(true);
        Time.timeScale = 0f;
        SettingsPanelIntro();
    }

    public async void OnResume()
    {
        _isTransitioning = true;
        await SettingsPanelOutro();
        background.SetActive(false);
        Time.timeScale = 1f;
        _isTransitioning = false;
    }

    public void OnMenuButton()
    {
        if (_isTransitioning) return;
        Time.timeScale = 1f;
        background.SetActive(false);
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void SettingsPanelIntro()
    {
        settingsPanelRect.DOAnchorPosY(middlePosY, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() => _isTransitioning = false);
    }

    private async Task SettingsPanelOutro()
    {
        await settingsPanelRect.DOAnchorPosY(bottomPosY, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .AsyncWaitForCompletion();
    }
}