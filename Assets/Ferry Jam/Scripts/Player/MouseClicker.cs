using UnityEngine;
using UnityEngine.InputSystem; // Added this

public class MouseClicker : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                ClickableObject clickedObject = hit.collider.GetComponent<ClickableObject>();
                
                if (clickedObject != null)
                {
                    clickedObject.OnClick();
                }
            }
        }
    }
}