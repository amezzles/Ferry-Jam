using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    public int health = 2;
    public GameObject[] cats; // Drag your 2 cat child objects into this list
    
    [Header("Settings")]
    public float invincibilityDuration = 1.5f; // Time player is safe after being hit
    public string hurtSound = ""; // Optional sound name

    private bool _isInvincible = false;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        // If we are currently invincible, ignore the hit
        if (_isInvincible) return;

        health--;

        // Play sound
        if (!string.IsNullOrEmpty(hurtSound) && AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(hurtSound);

        // Delete the cat! 
        // We use health as the index (if health is 1, we delete cats[1])
        if (health >= 0 && health < cats.Length)
        {
            if (cats[health] != null)
            {
                Destroy(cats[health]);
            }
        }

        // Check for Game Over
        if (health <= 0)
        {
            GameManager.Instance.LoadMainMenu();
        }
        else
        {
            // Start flickering and being invincible so we don't die instantly
            StartCoroutine(HandleInvincibility());
        }
    }

    private IEnumerator HandleInvincibility()
    {
        _isInvincible = true;

        // Flicker effect
        float timer = 0;
        while (timer < invincibilityDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled; // Flash on/off
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        _spriteRenderer.enabled = true; // Ensure sprite is back on
        _isInvincible = false;
    }
}