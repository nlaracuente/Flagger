using UnityEngine;
using UnityEngine.UI;

public class FlagCapturedPanel : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Text text;
    [SerializeField] GameObject alterPanel;

    private void Start()
    {
        ClosePanel();
        CloseAlertPanel();
    }

    public void OpenPanel(string message = "All Flags Collected!")
    {
        panel?.SetActive(true);
        if (text != null && !string.IsNullOrEmpty(message))
            text.text = message;
    }

    public void ClosePanel()
    {
        panel?.SetActive(false);
    }

    public void OpenAlertPanel()
    {
        alterPanel?.SetActive(true);
    }

    public void CloseAlertPanel()
    {
        alterPanel?.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        LevelController.Instance.GameCompleted();
    }
}
