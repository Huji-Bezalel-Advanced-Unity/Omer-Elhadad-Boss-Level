using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Serialization;

using System.Threading.Tasks;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private RectTransform settingsPanelRect;
    [SerializeField] private float bottomPosY, middlePosY;
    [SerializeField] private float duration = 0.5f;
    
    private const string MainMenuSceneName = "MainMenu"; // Todo : fix name when final scene is ready

    // private void Start()
    // {
    //     pauseMenuCanvas.SetActive(false);
    //     gameCanvas.SetActive(true);
    //     settingsCanvas.SetActive(false);
    // }
    
    private void Update()
    {
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
        background.SetActive(true);
        Time.timeScale = 0f;
        SettingsPanelIntro();
    }
    
    public async void OnResume()
    {
        await SettingsPanelOutro();
        background.SetActive(false);
        Time.timeScale = 1f;
        
        //gameCanvas.SetActive(true);
    }
    

    public void OnMenuButton()
    {
        Time.timeScale = 1f; // Resume the game
        background.SetActive(false);
        SceneManager.LoadScene(MainMenuSceneName);
    }
    
    private void SettingsPanelIntro()
    {
        settingsPanelRect.DOAnchorPosY(middlePosY, duration).SetEase(Ease.OutBack).SetUpdate(true);
    }
    
    private async Task SettingsPanelOutro()
    {
        await settingsPanelRect.DOAnchorPosY(bottomPosY, duration).SetEase(Ease.OutBack).SetUpdate(true).AsyncWaitForCompletion();
    }
}
