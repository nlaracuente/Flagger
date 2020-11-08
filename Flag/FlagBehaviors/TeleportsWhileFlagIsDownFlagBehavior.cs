/// <summary>
/// If the player's field of vision is colliding with the flag then it dissappears
/// </summary>
public class TeleportsWhileFlagIsDownFlagBehavior : FlagBehavior
{
    bool flagIsNormal = false;

    public TeleportsWhileFlagIsDownFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhileFlagIsDown;
        Hint = "What's down here?";
    }

    /// <summary>
    /// Hmm...we need to lower it...better set bools
    /// </summary>
    public override void OnStart()
    {
        base.OnStart();
        flagIsNormal = false;
        flag.FlagLowered();              
    }

    public override void OnPlayerInTriggerZone()
    {
        if (!flagIsNormal)
            flag.TriggerFlagTeleport();
    }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag()
    {
        MouseController.Instance.SetFingerCursor();
    }
    public override void OnMouseExistsFlag()
    {
        MouseController.Instance.SetHandCursor();
    }
    public override void OnMouseButtonUpFlag() 
    {
        flag.PlayClickSound();

        if (flagIsNormal)
        {
            flagIsNormal = false;
            flag.FlagLowered();
        }   
        else
        {
            flagIsNormal = true;
            flag.FlagIdled();
        }   
    }
}
