using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseFallSpeed = 5f;
    public float despawnY = -10f;

    [Header("Audio Settings")]
    public string spawnSoundName = ""; 

    void Start()
    {
        // --- DIAGNOSTIC LOGS ---
        if (AudioManager.Instance == null)
        {
            Debug.LogError("DIAGNOSTIC: AudioManager.Instance is NULL! The manager did not carry over to this scene.");
        }
        
        if (string.IsNullOrEmpty(spawnSoundName))
        {
            Debug.LogWarning("DIAGNOSTIC: spawnSoundName is EMPTY on this prefab: " + gameObject.name);
        }

        // The actual call
        if (!string.IsNullOrEmpty(spawnSoundName) && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(spawnSoundName);
        }
    }

    void Update()
    {
        float currentSpeed = baseFallSpeed * ObjectSpawner.GlobalSpeedMultiplier;
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

        if (transform.position.y < despawnY)
        {
            if (GameManager.Instance != null) GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }

    // Update ONLY this function in FallingObject.cs
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Look for the PlayerLife script on the object we hit
            PlayerLife life = other.GetComponent<PlayerLife>();

            if (life != null)
            {
                life.TakeDamage();
            
                // Destroy the obstacle so it doesn't hit the player multiple times
                Destroy(gameObject); 
            }
        }
    }
}