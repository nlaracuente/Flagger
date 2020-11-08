using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

/// <summary>
/// Abstract class to allow us to serialize so that we can use the 
/// editor to set the flag behaviors
/// </summary>
[System.Serializable]
public abstract class FlagBehavior : IFlagBehavior
{
    public enum Type {
        Normal,
        TeleportsContinuously,
        TeleportsWhileMouseNotOn,
        TeleportsWhilePlayerIsLooking,
        HidesWhilePlayerIsLooking,
        TeleportsWhilePlayerIsInLeftFieldOfVision,
        TeleportsWhilePlayerIsInRightFieldOfVision,
        TeleportsWhileIsWrongColor,
        TeleportsWaitUntilRightColor,
        TeleportsWhileFlagIsDown,
        TeleportsWhileFlagIsFlipped,
        TeleportsWhilePlayerIsNotLooking,
    }

    public Type FlagType { get; protected set; }
    public string Hint { get; protected set; }

    /// <summary>
    /// The parent component that contains this behavior
    /// This is to give us access to all the Monobehavior inheritence
    /// </summary>
    protected Flag flag;

    protected Transform transform;
    protected FlagPoints points;

    protected bool PlayerLookingAtFlag { get; set; }
    protected bool PlayerInFieldOfVision { get; set; }

    public FlagBehavior(Flag _parent)
    {
        flag = _parent;
        transform = flag.transform;
        points = Object.FindObjectOfType<FlagPoints>();
    }

    public void DebugStatus()
    {
        Debug.Log($"PlayerLookingAtFlag: {PlayerLookingAtFlag}");
        Debug.Log($"PlayerInFieldOfVision: {PlayerInFieldOfVision}");
    }

    public void MoveToRandomPoint()
    {
        transform.position = points.GetRandomPoint().position;
    }

    public virtual void OnStart()
    {
        MoveToRandomPoint();
        Flag.Instance.FlagUp();
    }

    public virtual void OnPlayerTouchesFlag()
    {
        Flag.Instance.FlagCaptured();
    }

    public abstract void OnPlayerInTriggerZone();
    public abstract void OnMouseButtonDownFlag();
    public abstract void OnMouseOverFlag();
    public abstract void OnMouseExistsFlag();
    public abstract void OnMouseButtonUpFlag();

    public virtual void OnPlayerLookingAtFlag()
    {
        PlayerLookingAtFlag = true;
    }

    public virtual void OnPlayerNotLookingAtFlag()
    {
        PlayerLookingAtFlag = false;
    }

    public virtual void OnPlayerInFieldOfVision()
    {
        PlayerInFieldOfVision = true;
    }

    public virtual void OnPlayerNotInFieldOfVision()
    {
        PlayerInFieldOfVision = false;
    }

}
