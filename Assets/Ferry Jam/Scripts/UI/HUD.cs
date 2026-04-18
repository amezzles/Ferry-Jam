using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUD : MonoBehaviour
{
    [Header("Button Icons")]
    public Sprite pauseIcon; 
    public Sprite playIcon;  

    private Button _pauseButton;
    private Button _quitButton;
    private Label _scoreLabel;
    private bool _isPaused = false;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Find the buttons
        _pauseButton = root.Q<Button>("Pause");
        _quitButton = root.Q<Button>("Quit"); // Make sure this name matches UI Builder!
        _scoreLabel = root.Q<Label>("Score");

        // --- Setup Pause Button ---
        if (_pauseButton != null)
        {
            _pauseButton.clicked += TogglePause;
            
            _pauseButton.text = ""; 
            if (pauseIcon != null)
            {
                _pauseButton.style.backgroundImage = new StyleBackground(pauseIcon);
            }
        }
        else
        {
            Debug.LogError("Could not find 'pause-button' in UI Builder!");
        }

        // --- Setup Quit Button ---
        if (_quitButton != null)
        {
            _quitButton.clicked += ReturnToMenu;
        }
        else
        {
            Debug.LogError("Could not find 'quit-button' in UI Builder!");
        }
    }
    
    void OnDisable()
    {
        if (_pauseButton != null) _pauseButton.clicked -= TogglePause;
        if (_quitButton != null) _quitButton.clicked -= ReturnToMenu;
    }

    private void Update()
    {
        if (_scoreLabel != null && GameManager.Instance != null)
        {
            _scoreLabel.text = "Score: " + GameManager.Instance.score;
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f; // Freeze
            if (playIcon != null) _pauseButton.style.backgroundImage = new StyleBackground(playIcon);
        }
        else
        {
            Time.timeScale = 1f; // Unfreeze
            if (pauseIcon != null) _pauseButton.style.backgroundImage = new StyleBackground(pauseIcon);
        }
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1f; 

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogError("GameManager is missing! Cannot return to menu.");
        }
    }
}