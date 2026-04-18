using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody2D))]
public class ContinuousWindMovement : MonoBehaviour
{
    [Header("Continuous Wind Settings")]
    [Tooltip("How hard the wind constantly pushes (needs to be higher than impulse)")]
    public float windForce = 60f; 
    
    [Tooltip("How close the mouse needs to be to affect the player")]
    public float maxWindRadius = 5f;

    private Rigidbody2D _rb;
    private Vector2 _mouseWorldPos;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // Zero gravity for top-down/floating
        _rb.gravityScale = 0f; 
        
        // Drag is still crucial! It stops them from floating away forever.
        _rb.linearDamping = 3f; 
    }

    void Update()
    {
        // 1. We read the mouse position in Update because it's tied to your screen's framerate
        if (Mouse.current == null) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
    }

    void FixedUpdate()
    {
        // 2. We apply the physics in FixedUpdate because it's a continuous force!
        Vector2 playerPos = transform.position;
        float distance = Vector2.Distance(_mouseWorldPos, playerPos);

        // If the mouse is close enough to the player...
        if (distance <= maxWindRadius)
        {
            // Calculate the direction FROM the mouse TO the player
            Vector2 pushDirection = (playerPos - _mouseWorldPos).normalized;

            // Make the push stronger the closer your mouse is to the player
            // (1 = mouse is exactly on player, 0 = mouse is at the edge of the radius)
            float distanceMultiplier = 1f - (distance / maxWindRadius);

            // Apply a CONSTANT force (not an impulse)
            _rb.AddForce(pushDirection * (windForce * distanceMultiplier), ForceMode2D.Force);
        }
    }
}