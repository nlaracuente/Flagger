using System.Collections;
using UnityEngine;

/// <summary>
/// Conitnues to teleport until the player captures it
/// However, it does not start teleporting unitl the player is close enough
/// </summary>
public class TeleportsContinuouslyFlagBehavior : FlagBehavior
{
    bool notCaptured = true; // for safety
    IEnumerator teleportRoutine;

    public TeleportsContinuouslyFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsContinuously;
        Hint = "Catch me if you can";
    }

    public override void OnStart()
    {
        notCaptured = true;
        teleportRoutine = TeleportRoutine();
        flag.StartCoroutine(teleportRoutine);
    }

    IEnumerator TeleportRoutine()
    {
        while (notCaptured)
        {
            MoveToRandomPoint();
            yield return flag.StartCoroutine(flag.FlagUpRoutine());

            // Now wait to teleport again
            if(notCaptured)
                yield return new WaitForSeconds(LevelController.Instance.teleportingFlagDelay);

            // Make sure to move the flag down before changing
            if (notCaptured)
                yield return flag.StartCoroutine(flag.FlagDownRoutine());
        }
    }

    public override void OnPlayerTouchesFlag()
    {
        notCaptured = false;
        flag.StopCoroutine(teleportRoutine);
        base.OnPlayerTouchesFlag();
    }
    public override void OnPlayerInTriggerZone() { }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }
}
