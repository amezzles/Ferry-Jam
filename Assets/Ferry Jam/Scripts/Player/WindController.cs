using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody2D))]
public class ContinuousWindMovement : MonoBehaviour
{
    [Header("Continuous Wind Settings")]
    public float windForce = 60f; 
    public float maxWindRadius = 5f;

    [Header("Audio Feedback")]
    public string windSoundName = "wind";
    [Tooltip("How close the mouse needs to be (0 to 1) to trigger the sound")]
    public float soundThreshold = 0.7f; 
    public float soundCooldown = 0.3f; // Prevents the sound from overlapping too much

    private Rigidbody2D _rb;
    private Vector2 _mouseWorldPos;
    private float _soundTimer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; 
        _rb.linearDamping = 3f; 
    }

    void Update()
    {
        if (Mouse.current == null) return;
        Vector2 screenPos = Mouse.current.position.ReadValue();
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Update the cooldown timer
        if (_soundTimer > 0) _soundTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector2 playerPos = transform.position;
        float distance = Vector2.Distance(_mouseWorldPos, playerPos);

        if (distance <= maxWindRadius)
        {
            Vector2 pushDirection = (playerPos - _mouseWorldPos).normalized;
            float distanceMultiplier = 1f - (distance / maxWindRadius);

            // Apply Force
            _rb.AddForce(pushDirection * (windForce * distanceMultiplier), ForceMode2D.Force);

            // --- AUDIO TRIGGER ---
            // If the push is strong enough AND the cooldown is over
            if (distanceMultiplier >= soundThreshold && _soundTimer <= 0)
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(windSoundName);
                    _soundTimer = soundCooldown; // Reset the timer
                }
            }
        }
    }
}