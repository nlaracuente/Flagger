using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOrderByPosition : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = (int)transform.position.y;
    }
}
