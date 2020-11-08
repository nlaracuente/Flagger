using System.Collections;
using UnityEngine;

/// <summary>
/// If the player's field of vision is colliding with the flag then it dissappears
/// </summary>
public class TeleportsWaitUntilRightColorFlagBehavior : FlagBehavior
{
    bool notCaptured = true; // for safety
    IEnumerator changeColorRoutine;

    public TeleportsWaitUntilRightColorFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWaitUntilRightColor;
        Hint = "I don't feel so good...";
    }
    public override void OnStart()
    {
        base.OnStart();
        changeColorRoutine = ChangeColorRoutine();
        flag.StartCoroutine(changeColorRoutine);        
    }

    IEnumerator ChangeColorRoutine()
    {
        while (notCaptured)
        {
            if(!flag.IsWrongColor)
                flag.SetRandomFlagColor();
            else
                flag.SetRightFlagColor();

            yield return new WaitForSeconds(LevelController.Instance.colorChangeFlagDelay);
        }
    }

    public override void OnPlayerTouchesFlag()
    {
        notCaptured = false;
        flag.StopCoroutine(changeColorRoutine);
        base.OnPlayerTouchesFlag();        
    }
    public override void OnPlayerInTriggerZone()
    {
        if (flag.IsWrongColor)
            flag.TriggerFlagTeleport();
    }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }
}
