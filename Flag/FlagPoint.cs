using UnityEngine;

/// <summary>
/// A point where the flag can appear from
/// It can also detect if the player is within its range
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class FlagPoint : MonoBehaviour
{
    public bool PlayerIsClose { get; private set; }

    private void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            PlayerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerIsClose = false;
    }
}
