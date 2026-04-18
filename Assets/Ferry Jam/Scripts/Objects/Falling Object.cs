using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float baseFallSpeed = 5f;
    public float despawnY = -10f;

    void Update()
    {
        // Multiply speed by the global difficulty from the Spawner
        float currentSpeed = baseFallSpeed * ObjectSpawner.GlobalSpeedMultiplier;

        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

        if (transform.position.y < despawnY)
        {
            if (GameManager.Instance != null) GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
    
    // Safety check: Reset time if player hits obstacle
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the PlayerLife script on the object we hit
            PlayerLife life = other.GetComponent<PlayerLife>();
        
            if (life != null)
            {
                life.TakeDamage();
            }

            // Always destroy the obstacle when it hits the player
            Destroy(gameObject);
        }
    }
}