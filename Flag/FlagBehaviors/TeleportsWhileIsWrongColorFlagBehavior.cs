/// <summary>
/// If the player's field of vision is colliding with the flag then it dissappears
/// </summary>
public class TeleportsWhileIsWrongColorFlagBehavior : FlagBehavior
{
    public TeleportsWhileIsWrongColorFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhileIsWrongColor;
        Hint = "Do you like it?";
    }

    public override void OnStart()
    {
        flag.SetRandomFlagColor();
        base.OnStart();        
    }

    public override void OnPlayerInTriggerZone()
    {
        if (flag.IsWrongColor)
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
        if (flag.IsWrongColor)
            flag.SetRightFlagColor();
        else
            flag.SetRandomFlagColor();
    }
}
