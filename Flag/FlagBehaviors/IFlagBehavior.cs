using UnityEngine.EventSystems;

public interface IFlagBehavior
{
    void OnPlayerInTriggerZone();
        
    void OnPlayerLookingAtFlag();
    void OnPlayerNotLookingAtFlag();

    void OnPlayerInFieldOfVision();
    void OnPlayerNotInFieldOfVision();   

    void OnPlayerTouchesFlag();

    void OnMouseButtonDownFlag();
    void OnMouseOverFlag();
    void OnMouseExistsFlag();
    void OnMouseButtonUpFlag();
}
