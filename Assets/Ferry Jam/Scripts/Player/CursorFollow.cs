using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollow : MonoBehaviour
{
    void Start()
    {
        // Hide the default system cursor
        Cursor.visible = false;
    }

    void Update()
    {
        // Safety check for the New Input System
        if (Mouse.current == null) return;

        // 1. Get the mouse position on the screen
        Vector2 screenPos = Mouse.current.position.ReadValue();

        // 2. Convert it to a position in your 2D game world
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // 3. Move this GameObject to that position
        // We set Z to 0 to ensure it's visible in a 2D game
        transform.position = new Vector3(worldPos.x, worldPos.y, 0f);
    }

    // A good practice: make the cursor visible again if the game quits
    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }
}