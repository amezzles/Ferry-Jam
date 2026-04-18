using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(UIDocument))]
public class HUD : MonoBehaviour
{
    [Header("Button Icons")]
    public Sprite pauseIcon; 
    public Sprite playIcon;  

    private Button _pauseButton;
    private Button _quitButton; 
    private bool _isPaused = false;
    private VisualElement _root;

    void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _pauseButton = _root.Q<Button>("Pause");
        _quitButton = _root.Q<Button>("Quit"); 

        if (_pauseButton != null)
        {
            _pauseButton.clicked += TogglePause;
            _pauseButton.text = ""; 
            if (pauseIcon != null) _pauseButton.style.backgroundImage = new StyleBackground(pauseIcon);
        }

        if (_quitButton != null) _quitButton.clicked += ReturnToMenu;
    }
    
    void OnDisable()
    {
        if (_pauseButton != null) _pauseButton.clicked -= TogglePause;
        if (_quitButton != null) _quitButton.clicked -= ReturnToMenu;
    }

    void Update()
    {
        // Check for Escape key press
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Only allow pausing if the Game Over screen isn't showing
            // We check this by seeing if the HUD is still visible
            if (_root.style.display != DisplayStyle.None)
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f; // Freeze
            
            // Show the real mouse so we can click "Quit"
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (playIcon != null) _pauseButton.style.backgroundImage = new StyleBackground(playIcon);
        }
        else
        {
            Time.timeScale = 1f; // Unfreeze
            
            // Hide the real mouse again for the Puff cursor
            Cursor.visible = false;

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
    }
}