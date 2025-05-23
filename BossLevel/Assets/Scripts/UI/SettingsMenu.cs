using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject lastActiveCanvas;
    
    public void OnBackButton()
    {
        gameObject.SetActive(false);
        lastActiveCanvas.SetActive(true);
    }
}
