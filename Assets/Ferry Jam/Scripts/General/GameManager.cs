using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene Configuration")]
    public string mainMenuSceneName = "MainMenu";
    public string mainGameSceneName = "Game";

    [Header("Game State")]
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

    // --- SCENE LOADING ---

    public void LoadGame()
    {
        score = 0;
        Time.timeScale = 1f;
        
        // Pick random music for the gameplay
        if (AudioManager.Instance != null) AudioManager.Instance.PlayRandomMusic();
        
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        
        // Reset Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pick random music for the menu
        if (AudioManager.Instance != null) AudioManager.Instance.PlayRandomMusic();

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // --- GAME LOGIC ---

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // Freeze the game
        Time.timeScale = 0f;

        // Stop the music
        if (AudioManager.Instance != null) AudioManager.Instance.StopMusic();

        // Hide the HUD
        HUD hud = Object.FindAnyObjectByType<HUD>();
        if (hud != null)
        {
            hud.GetComponent<UnityEngine.UIElements.UIDocument>().rootVisualElement.style.display = UnityEngine.UIElements.DisplayStyle.None;
        }

        // Show the Game Over Screen
        GameOverUI gameOverUI = Object.FindAnyObjectByType<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.Show(score);
        }

        yield break; // Coroutine ends here; the UI buttons take over
    }
}