using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        startButton.onClick.RemoveListener(OnStartButtonClick);
        quitButton.onClick.RemoveListener(OnQuitButtonClick);
        
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private static void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private static void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
