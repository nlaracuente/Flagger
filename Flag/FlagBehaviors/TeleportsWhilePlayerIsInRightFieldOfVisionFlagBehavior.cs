/// <summary>
/// Remains hidden while the player is within the RedZone
/// </summary>
public class TeleportsWhilePlayerIsInRightFieldOfVisionFlagBehavior : FlagBehavior
{
    public TeleportsWhilePlayerIsInRightFieldOfVisionFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhilePlayerIsInRightFieldOfVision;
        Hint = "I thought you left";
    }

    public override void OnStart()
    {
        base.OnStart();
        Flag.Instance.EnableRightFOV();
    }

    public override void OnPlayerInTriggerZone()
    {
        if (PlayerInFieldOfVision)
            Flag.Instance.TriggerFlagTeleport();
    }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }    
}
