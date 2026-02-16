using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindAnyObjectByType<UIManager>();
            }

            return _instance;
        }
    }

    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI bombCountTMP;

    public Button pauseButton;
    public Button resumeButton;

    public GameObject gameOverPlane;
    public TextMeshProUGUI bestScoreTMP;
    public TextMeshProUGUI currentScoreTMP;
    
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
        quitButton.onClick.RemoveListener(OnQuitButtonClick);
        
        restartButton.onClick.AddListener(OnRestartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    public void UpdateScoreUI(int score)
    {
        scoreTMP.text = score.ToString();
    }
    
    public void UpdateBombCountUI(int bombCount)
    {
        bombCountTMP.text = bombCount.ToString();
    }

    void OnPauseButtonClick()
    {
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        GameManager.Instance.PauseGame();
        AudioManager.Instance.PlayButtonClip();
    }
    
    void OnResumeButtonClick()
    {
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        GameManager.Instance.ResumeGame();
        AudioManager.Instance.PlayButtonClip();
    }
    
    void OnRestartButtonClick()
    {
        GameManager.Instance.RestartGame();
        AudioManager.Instance.PlayButtonClip();
    }
    
    void OnQuitButtonClick()
    {
        GameManager.Instance.QuitGame();
        AudioManager.Instance.PlayButtonClip();
    }
    
    public void ShowGameOverPlane(int bestScore, int currentScore)
    {
        gameOverPlane.SetActive(true);
        bestScoreTMP.text = bestScore.ToString();
        currentScoreTMP.text = currentScore.ToString();
    }
}
