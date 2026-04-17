using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUD : MonoBehaviour
{
    private VisualElement _pauseMenu;
    private Button _pauseButton;
    private Button _resumeButton;
    private Button _menuButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _pauseButton = root.Q<Button>("Pause");
        _resumeButton = root.Q<Button>("Play");
        _menuButton = root.Q<Button>("Quit");
        
        _pauseButton.clicked += PauseGame;
        _resumeButton.clicked += ResumeGame;
        _menuButton.clicked += ReturnToMenu;
    }
    
    void OnDisable()
    {
        _pauseButton.clicked -= PauseGame;
        _resumeButton.clicked -= ResumeGame;
        _menuButton.clicked -= ReturnToMenu;
    }

    private void PauseGame()
    {
        _pauseMenu.style.display = DisplayStyle.Flex;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        _pauseMenu.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadMainMenu();
    }
}