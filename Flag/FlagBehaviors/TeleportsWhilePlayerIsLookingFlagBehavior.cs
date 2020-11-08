/// <summary>
/// If the player's field of vision is colliding with the flag then it dissappears
/// </summary>
public class TeleportsWhilePlayerIsLookingFlagBehavior : FlagBehavior
{
    public TeleportsWhilePlayerIsLookingFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhilePlayerIsLooking;
        Hint = "It's rude to stare";
    }

    public override void OnPlayerTouchesFlag()
    {
        if (PlayerLookingAtFlag)
            Flag.Instance.TriggerFlagTeleport();
        else
            base.OnPlayerTouchesFlag();
    }

    public override void OnPlayerInTriggerZone() { }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }    
}
