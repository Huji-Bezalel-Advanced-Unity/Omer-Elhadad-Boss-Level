using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string CutSceneName = "StoryBoardCutScene"; // Todo : fix name when final scene is ready
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    
    
    private void Start()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    
    public void OnSettingsButton()
    {
        settingsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    
    public void OnBackButton()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    
    public void OnQuitGameButton()
    {
        Application.Quit();
    }

    public void OnStartGameButton()
    {
        SceneManager.LoadScene(CutSceneName);
    }
}
