using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An empty state
/// </summary>
public class AiState_Idle : IState
{


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

    public AiState_Idle(int ID)
    {
        _stateID = ID;
    }
}
