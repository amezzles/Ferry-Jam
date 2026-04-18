using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene Configuration")]
    [Tooltip("The name of your main menu scene file.")]
    public string mainMenuSceneName = "MainMenu";

    [Tooltip("The name of your main game scene file.")]
    public string mainGameSceneName = "MainGame";
    
    // Inside GameManager.cs
    public int score = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }
    
    public void LoadMainMenu()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None; 
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
    // Inside GameManager.cs

    public void GameOver()
    {
        Time.timeScale = 0f; // Freeze game

        // 1. Hide the HUD
        HUD hud = Object.FindAnyObjectByType<HUD>();
        if (hud != null)
        {
            hud.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        }

        // 2. Show the Game Over Screen
        GameOverUI gameOverUI = Object.FindAnyObjectByType<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.Show(score);
        }
    }
    
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}