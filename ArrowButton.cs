using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ArrowButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    ArrowDirection direction;
    public ArrowDirection Direction { get { return direction; } }

    public Button Button { get; private set; }
    ArrowController arrowController;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    private void Start()
    {
        arrowController = FindObjectOfType<ArrowController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Button.interactable)
            arrowController.OnArrowButtonClicked(this);
    }
}

public enum ArrowDirection
{
    Up,
    Down,
    Left,
    Right
}
