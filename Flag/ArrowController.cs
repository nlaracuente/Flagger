using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    bool reenableButtons = false;

    [SerializeField, Tooltip("In Seconds")]
    int reenableDelay = 2;

    Dictionary<ArrowDirection, ArrowButton> arrowButtons;
    public List<ArrowDirection> ActiveDirections 
    { 
        get { 
            return arrowButtons.Where(kvp => kvp.Value.Button.interactable).Select(k => k.Key).ToList(); 
        } 
    }

    Player_OldVersion player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_OldVersion>();
        arrowButtons = new Dictionary<ArrowDirection, ArrowButton>();
        foreach (var button in FindObjectsOfType<ArrowButton>())
        {
            arrowButtons.Add(button.Direction, button);
        }
    }

    private void Update()
    {
        ArrowButton arrow = null;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            arrow = h < 0 ? arrowButtons[ArrowDirection.Left] : arrowButtons[ArrowDirection.Right];
        }
        else if (v != 0)
        {
            arrow = v < 0 ? arrowButtons[ArrowDirection.Down] : arrowButtons[ArrowDirection.Up];
        }

        if (arrow != null && arrow.Button.interactable)
            OnArrowButtonClicked(arrow);

    }

    public void OnArrowButtonClicked(ArrowButton arrow)
    {
        // Must have an actual button not already disabled
        if (arrow == null || !arrow.Button.interactable)
            return;

        player.CancelMovement();
        arrow.Button.interactable = false;
        StartCoroutine(OnArrowButtonClickedRoutine(arrow));
    }

    IEnumerator OnArrowButtonClickedRoutine(ArrowButton arrow)
    {
        yield return new WaitForSeconds(reenableDelay);
        arrow.Button.interactable = true;
    }
}
