using UnityEngine;

public class RotateLookAtMouse : MonoBehaviour
{
    [SerializeField] bool ignorePause = false;

    void Update()
    {
        if (!ignorePause && MenuController.Instance.IsGamePaused)
            return;

        var mDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(mDir.y, mDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
