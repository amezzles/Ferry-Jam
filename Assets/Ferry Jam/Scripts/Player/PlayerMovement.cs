using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true; 
        _rb.gravityScale = 0f; 
    }

    void Update()
    {
        // Safety check
        if (Keyboard.current == null) return;

        float x = 0;
        float y = 0;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) y = 1;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) y = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1;

        _movement = new Vector2(x, y);
    }

    void FixedUpdate()
    {
        _rb.velocity = _movement.normalized * moveSpeed;
    }
}