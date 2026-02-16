using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindAnyObjectByType<GameManager>();
            }

            return _instance;
        }
    }

    private int _bombCount;
    private int _score;
    private GameState _gameState = GameState.Playing;

    private const float DoubleClickThreshold = 0.2f;
    private float _lastClickTime;

    public AudioSource useBombAudio;
    public AudioSource gameOverAudio;

    void Start()
    {
        ResumeGame();
    }
    
    void Update()
    {
        UseBombUpdate();
    }

    public void AddBomb()
    {
        _bombCount++;
        UIManager.Instance.UpdateBombCountUI(_bombCount);
    }

    private void SubBomb()
    {
        _bombCount--;
        UIManager.Instance.UpdateBombCountUI(_bombCount);
    }

    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.UpdateScoreUI(_score);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _gameState = GameState.Pause;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _gameState = GameState.Playing;
    }

    public bool IsPause()
    {
        return _gameState == GameState.Pause;
    }

    void UseBombUpdate()
    {
        var mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (Time.time - _lastClickTime < DoubleClickThreshold)
            {
                UseBomb();
            }
            else
            {
                _lastClickTime = Time.time;
            }
        }
    }

    void UseBomb()
    {
        if (_bombCount <= 0) return;
        
        useBombAudio.Play();
        
        SubBomb();
        
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy e in enemies)
        {
            e.KillEnemy();
        }
    }
    
    public void GameOver()
    {
        if (_gameState == GameState.GameOver) return;
        
        PauseGame();
        _gameState = GameState.GameOver;
        
        gameOverAudio.Play();
        
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UIManager.Instance.ShowGameOverPlane(bestScore, _score);
        
        if (_score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _score);
        }
    }

    public void RestartGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}

