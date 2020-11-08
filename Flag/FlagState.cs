
/// <summary>
/// Define the states that the Flag could be on
/// Multiple of these states can be on at the same time
/// However, there are some states that clash
/// </summary>
public struct FlagState
{
    bool isRaised;
    public bool IsRaised
    {
        get { return isRaised; }
        set {
            isLowered = !value;
            isRaised = value;
        }
    }

    bool isLowered;
    public bool IsLowered
    {
        get { return isLowered; }
        set
        {
            isRaised = !value;
            isLowered = value;
        }
    }

    public bool IsFlipped { get; set; }
    public bool IsWrongColor { get; set; }

    public bool IsMouseOver { get; set; }
    public bool IsMouseDown { get; set; }

    public bool IsPlayerInFOV { get; set; }
    public bool IsPlayerLookingAtFlag { get; set; }
}
