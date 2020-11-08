public class HidesWhilePlayerIsLookingFlagBehavior : FlagBehavior
{
    public HidesWhilePlayerIsLookingFlagBehavior(Flag _parent) : base(_parent)
    {
        FlagType = Type.HidesWhilePlayerIsLooking;
        Hint = "Stop...You are embarrassing me";
    }

    public override void OnPlayerLookingAtFlag()
    {
        base.OnPlayerLookingAtFlag();
        flag.FlagDown(false);
    }

    public override void OnPlayerNotLookingAtFlag()
    {
        base.OnPlayerNotLookingAtFlag();
        flag.FlagUp();
    }

    public override void OnPlayerInTriggerZone() { }
    public override void OnMouseButtonDownFlag() { }
    public override void OnMouseOverFlag() { }
    public override void OnMouseExistsFlag() { }
    public override void OnMouseButtonUpFlag() { }
}
