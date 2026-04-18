using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    public int health = 2;
    public GameObject[] cats; 
    
    [Header("Audio")]
    public string hurtSound = "hurt"; 
    public string endSound = "end";   

    [Header("Settings")]
    public float invincibilityDuration = 1.5f; 

    private bool _isInvincible = false;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        if (_isInvincible) return;

        health--;

        // 1. Play Hurt Sound
        if (health > 0)
        {
            if (!string.IsNullOrEmpty(hurtSound) && AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(hurtSound);
        }

        // 2. Remove a cat visual
        if (health >= 0 && health < cats.Length)
        {
            if (cats[health] != null) cats[health].SetActive(false);
        }

        // 3. Check for Game Over
        if (health <= 0)
        {
            // Play End Sound
            if (!string.IsNullOrEmpty(endSound) && AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(endSound);

            // Trigger the cool freeze-frame sequence in GameManager
            GameManager.Instance.GameOver();
        }
        else
        {
            StartCoroutine(HandleInvincibility());
        }
    }

    private IEnumerator HandleInvincibility()
    {
        _isInvincible = true;
        float timer = 0;
        while (timer < invincibilityDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled; 
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        _spriteRenderer.enabled = true; 
        _isInvincible = false;
    }
}