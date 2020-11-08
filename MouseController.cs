using UnityEngine;

public class MouseController : Singleton<MouseController>
{
    [SerializeField] Texture2D fingerCursor;
    [SerializeField] Texture2D fistCursor;
    [SerializeField] Texture2D handCursor;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetFingerCursor()
    {
        Cursor.SetCursor(fingerCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetFistCursor()
    {
        Cursor.SetCursor(fistCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetHandCursor()
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }
}
