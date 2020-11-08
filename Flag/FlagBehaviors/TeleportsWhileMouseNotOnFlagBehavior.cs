/// <summary>
/// Mouse must remain over the flag to be captured
/// </summary>
public class TeleportsWhileMouseNotOnFlagBehavior : FlagBehavior
{
    private bool isMouseOn = false;

    public TeleportsWhileMouseNotOnFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.TeleportsWhileMouseNotOn;
        Hint = "Hold me...";
    }
   
    public override void OnPlayerInTriggerZone() 
    {
        if (!isMouseOn)
            Flag.Instance.TriggerFlagTeleport();
    }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() 
    { 
        isMouseOn = true;
        MouseController.Instance.SetFistCursor();
    }
    public override void OnMouseExistsFlag() 
    { 
        isMouseOn = false;
        MouseController.Instance.SetHandCursor();
    }
    public override void OnMouseButtonUpFlag() { }
}
