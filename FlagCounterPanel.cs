using UnityEngine;
using UnityEngine.UI;

public class FlagCounterPanel : MonoBehaviour
{
    [SerializeField] Text counter;

    private void Update()
    {
        counter.text = $"{Flag.Instance.TotalCompleted} / {Flag.Instance.TotalBehaviors}";
    }
}
