using UnityEngine;
using UnityEngine.UI;

public class HintSystemPanel : Singleton<HintSystemPanel>
{
    [SerializeField] Text hint;

    public void SetHint(string text)
    {
        hint.text = text;
    }
}
