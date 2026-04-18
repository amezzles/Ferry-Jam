using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class CursorFollow : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        // Hide the default system cursor
        Cursor.visible = false;
        
        // Get the reference to our own sprite
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if the game is paused
        bool isPaused = Time.timeScale == 0f;

        // Hide our sprite if the game is paused, show it if not.
        _spriteRenderer.enabled = !isPaused;

        // If the game is paused, do nothing else.
        if (isPaused)
        {
            return;
        }

        // --- Only run this code if the game is NOT paused ---
        if (Mouse.current == null) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0f);
    }

    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }
}