/// <summary>
/// Remains hidden while the player is within the RedZone
/// </summary>
public class TeleportsWhilePlayerIsInLeftFieldOfVisionFlagBehavior : FlagBehavior
{
    public TeleportsWhilePlayerIsInLeftFieldOfVisionFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhilePlayerIsInLeftFieldOfVision;
        Hint = "Do it the right way";
    }

    public override void OnStart()
    {
        base.OnStart();
        Flag.Instance.EnableLeftFOV();
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
