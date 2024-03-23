using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move to random positions that are a greater than a given distance away
/// </summary>
public class AiState_Roaming : IState
{
    public float minDistanceToTarget = 5;
    public float maxDistanceToTarget = 10;
    AiController controller;
    Targeter moveTargeter;

    int _stateID;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        controller.possessedPawn.lookTarget.setTarget(moveTargeter.transform);
        moveTargeter.transform.position = randPosAroundPawn();

        Debug.Log("Starting roaming");
    }

    public void OnEnd()
    {
        
    }

    public void Tick()
    {
    }

    Vector3 randPosAroundPawn()
    {
        //Generate a random direction
        Vector2 dir = Vector2.zero.Randomize();

        float randDistance = Random.Range(minDistanceToTarget, maxDistanceToTarget);

        Vector3 randPosition = controller.possessedPawn.transform.position + (new Vector3(dir.x, 0, dir.y) * randDistance);

        return randPosition;
    }

    public AiState_Roaming(int ID, AiController Controller, Targeter MoveTargeter, float MinDistance, float MaxDistance) { 
        minDistanceToTarget = MinDistance;
        maxDistanceToTarget = MaxDistance;
        moveTargeter = MoveTargeter;
        controller = Controller;

        _stateID = ID; 
    }
}
