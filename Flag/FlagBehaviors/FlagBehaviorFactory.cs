using UnityEngine;
using System.Collections;

public static class FlagBehaviorFactory
{
    public static FlagBehavior Create(FlagBehavior.Type type, Flag _parent)
    {
        FlagBehavior behavior = null;

        switch(type)
        {
            case FlagBehavior.Type.Normal:
                behavior = new NormalFlagBehavior(_parent);
                break;            

            case FlagBehavior.Type.TeleportsContinuously:
                behavior = new TeleportsContinuouslyFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhileMouseNotOn:
                behavior = new TeleportsWhileMouseNotOnFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhilePlayerIsLooking:
                behavior = new TeleportsWhilePlayerIsLookingFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.HidesWhilePlayerIsLooking:
                behavior = new HidesWhilePlayerIsLookingFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhilePlayerIsInLeftFieldOfVision:
                behavior = new TeleportsWhilePlayerIsInLeftFieldOfVisionFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhilePlayerIsInRightFieldOfVision:
                behavior = new TeleportsWhilePlayerIsInRightFieldOfVisionFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhileIsWrongColor:
                behavior = new TeleportsWhileIsWrongColorFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWaitUntilRightColor:
                behavior = new TeleportsWaitUntilRightColorFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhileFlagIsDown:
                behavior = new TeleportsWhileFlagIsDownFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhileFlagIsFlipped:
                behavior = new TeleportsWhileFlagIsFlippedFlagBehavior(_parent);
                break;

            case FlagBehavior.Type.TeleportsWhilePlayerIsNotLooking:
                behavior = new TeleportsWhilePlayerIsNotLookingFlagBehavior(_parent);
                break;
        }

        return behavior;
    }
}
