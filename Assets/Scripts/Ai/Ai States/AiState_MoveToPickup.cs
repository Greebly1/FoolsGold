using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move to a pickup
/// </summary>
public class AiState_MoveToPickup : IState
{
    AiController controller;
    AiSight senses;
    Targeter moveTargeter;

    int _stateID;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        moveTargeter.setTarget(controller.selectedTarget.transform);
        controller.possessedPawn?.lookTarget.setTarget(controller.selectedTarget.transform);
    }

    public void OnEnd()
    {
        moveTargeter.setFollow(false);
        controller.possessedPawn?.lookTarget.setFollow(false);
    }

    public void Tick()
    {
        
    }

    public AiState_MoveToPickup(int ID, AiController Controller)
    {
        controller = Controller;
        _stateID = ID;
    }
}
