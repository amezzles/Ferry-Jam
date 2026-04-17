using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene Configuration")]
    [Tooltip("The name of your main menu scene file.")]
    public string mainMenuSceneName = "MainMenu";

    [Tooltip("The name of your main game scene file.")]
    public string mainGameSceneName = "MainGame";

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
    
    public void LoadGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}