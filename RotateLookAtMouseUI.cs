using UnityEngine;
using UnityEngine.UI;

public class RotateLookAtMouseUI : MonoBehaviour
{
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var mDir = Input.mousePosition - rectTransform.position;
        var angle = Mathf.Atan2(mDir.y, mDir.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
