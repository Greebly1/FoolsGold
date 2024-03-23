using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interact with a thingamabob
/// </summary>
public class AiState_Interact : IState
{
    AiController controller;

    int _stateID;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        (controller.possessedPawn as HumanoidPawn).Interact();
    }

    public void OnEnd()
    {

    }

    public void Tick()
    {

    }

    public AiState_Interact(int ID, AiController Controller)
    {
        controller = Controller;
        _stateID = ID;
    }
}
