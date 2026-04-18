using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(UIDocument))]
public class GameOverUI : MonoBehaviour
{
    private VisualElement _root;
    private Button _retryButton;
    private Button _menuButton;
    private Label _finalScoreLabel;

    void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        // Find elements by name - Make sure these match your UXML!
        _retryButton = _root.Q<Button>("Retry");
        _menuButton = _root.Q<Button>("MainMenu");
        _finalScoreLabel = _root.Q<Label>("FinalScore");

        if (_retryButton != null) _retryButton.clicked += OnRetryClicked;
        if (_menuButton != null) _menuButton.clicked += OnMenuClicked;

        // Hide by default
        _root.style.display = DisplayStyle.None;
    }

    public void Show(int score)
    {
        // Make the mouse visible so they can click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _root.style.display = DisplayStyle.Flex;
        
        if (_finalScoreLabel != null)
            _finalScoreLabel.text = "Final Score: " + score;
    }

    private void OnRetryClicked()
    {
        Time.timeScale = 1f;
        
        if (AudioManager.Instance != null) AudioManager.Instance.PlayRandomMusic();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnMenuClicked()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadMainMenu();
    }
    
}