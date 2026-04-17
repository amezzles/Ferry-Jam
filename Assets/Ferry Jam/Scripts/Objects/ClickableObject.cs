using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ClickableObject : MonoBehaviour
{
    [Header("What happens when I am clicked?")]
    public UnityEvent onClickAction;
    
    public string clickSoundName = "click";

    public void OnClick()
    {
        onClickAction.Invoke();

        if (!string.IsNullOrEmpty(clickSoundName) && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clickSoundName);
        }
    }
}