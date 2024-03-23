using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set the looktarget, then fire at the target if it is in line of sight
/// </summary>
public class AIState_Attack : IState
{
    AiController controller;
    int _stateID;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        
    }

    public void OnEnd()
    {
        
    }

    public void Tick()
    {
        
    }



    public AIState_Attack(int ID, AiController Controller)
    {
        controller = Controller;
        _stateID = ID;
    }
}
