using UnityEngine;

/// <summary>
/// A trigger that detects when the player has enter/exit it and notifies the flag
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FlagTrigger : MonoBehaviour
{
    Collider2D Collider { get; set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flag.Instance.OnFlagTriggerEntered(this, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Flag.Instance.OnFlagTriggerStay(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Flag.Instance.OnFlagTriggerExit(this, collision);
    }
}
