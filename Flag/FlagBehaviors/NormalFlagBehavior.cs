/// <summary>
/// No gimmicks - just touch to collect
/// </summary>
public class NormalFlagBehavior : FlagBehavior
{
    public NormalFlagBehavior(Flag _parent) : base(_parent) 
    {
        FlagType = Type.Normal;
        Hint = "Collect me, if you dare...";
    }

    public override void OnPlayerInTriggerZone() { }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }
}
