using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    private Button _startButton;
    private Button _quitButton;
    
    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        _startButton = root.Q<Button>("Start");
        _quitButton = root.Q<Button>("Quit");
        
        _startButton.clicked += OnStartClicked;
        _quitButton.clicked += OnQuitClicked;
    }

    void OnDisable()
    {
        _startButton.clicked -= OnStartClicked;
        _quitButton.clicked -= OnQuitClicked;
    }

    private void OnStartClicked()
    {
        Debug.Log("Start button clicked. Loading game scene...");
        GameManager.Instance.LoadGame();
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit button clicked.");
        GameManager.Instance.QuitGame();
    }
}