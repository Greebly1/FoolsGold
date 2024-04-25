using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Move to random positions that are a greater than a given distance away
/// </summary>
public class AiState_Roaming : IState
{
    public float minDistanceToTarget = 5;
    public float maxDistanceToTarget = 10;
    public float minWaitTime = 5;
    public float maxWaitTime = 10;
    AiController controller;
    Targeter moveTargeter;
    Vector3 roamPosition = Vector3.zero;
    float waitTimer = 0;
    bool isAtPosition
    {
        get
        {
            return Vector3.Distance(roamPosition, controller.possessedPawn.transform.position) <  5;
        }
    }

    int _stateID;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        controller.possessedPawn.lookTarget.setTarget(moveTargeter.transform);
        Roam();

        Debug.Log("Starting roaming");
    }

    public void OnEnd()
    {
        
    }

    public void Tick()
    {
        if (isAtPosition)
        {
            Debug.Log("im doing it");
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
                return; //early out
            } //else
            
            Roam();
        } //code block skips if we are at the position


    }

    //generate a new position, set the move targeter to the position
    void Roam()
    {
        roamPosition = RandomPositionForRoaming();
        moveTargeter.transform.position = roamPosition;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    //Roaming position weighted to be slightly closer to the player
    /// <summary>
    /// This turned out to be a dumb idea, because it was possible for the new position to be unnavigable
    /// </summary>
    /// <returns></returns>
    Vector3 RandomPositionForRoaming()
    {
        Vector3 targetPos;

        if (PlayerController.ClientPlayerController.possessedPawn != null)
        {
            targetPos = PlayerController.ClientPlayerController.possessedPawn.transform.position;
        }
        else
        {
            targetPos = new Vector3(0, 0, 0);
        }

        float lerpAlpha = 0.5f;

        int MAX_ITERATIONS = 100;
        int ITERATIONS = 0;
        while (true && ITERATIONS < MAX_ITERATIONS)
        {
            Vector3 pawnRoamPos = randPosAroundObject(controller.possessedPawn.transform.position);
            Vector3 targetRoamPos = randPosAroundObject(targetPos);
            Vector3 randPosition = new Vector3(
                x: Mathf.Lerp(pawnRoamPos.x, targetRoamPos.x, lerpAlpha),
                y: Mathf.Lerp(pawnRoamPos.y, targetRoamPos.y, lerpAlpha),
                z: Mathf.Lerp(pawnRoamPos.z, targetRoamPos.z, lerpAlpha)
                );

            var agent = controller.navAgent;
            var path = new NavMeshPath();
            agent.CalculatePath(randPosition, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    return randPosition;
                case NavMeshPathStatus.PathPartial:
                    break;
                default:
                    break;
            }

            ITERATIONS++;
        }

        return new Vector3(0, 0, 0);

    }

    Vector3 randNavigablePosAroundPawn()
    {

        int MAX_ITERATIONS = 100;
        int ITERATIONS = 0;
        while (true && ITERATIONS < MAX_ITERATIONS)
        {
            Vector3 randPosition = randPosAroundObject(controller.possessedPawn.transform.position);

            var agent = controller.navAgent;
            var path = new NavMeshPath();
            agent.CalculatePath(randPosition, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    return randPosition;
                case NavMeshPathStatus.PathPartial:
                    break;
                default:
                    break;
            }

            ITERATIONS++;
        }

        return new Vector3(0, 0, 0);
    }

    Vector3 randPosAroundObject(Vector3 targetPos)
    {
        //Generate a random direction
        Vector2 dir = Vector2.zero.Randomize();

        float randDistance = Random.Range(minDistanceToTarget, maxDistanceToTarget);

        Vector3 randPosition = targetPos + (new Vector3(dir.x, 0, dir.y) * randDistance);

        return randPosition;
    }

    Vector3 randPosAroundTarget()
    {
        Vector3 targetPos;

        if (PlayerController.ClientPlayerController.possessedPawn != null)
        {
            targetPos = PlayerController.ClientPlayerController.possessedPawn.transform.position;
        }
        else
        {
            targetPos = new Vector3(0, 0, 0);
        }

        int MAX_ITERATIONS = 100;
        int ITERATIONS = 0;
        while (true && ITERATIONS < MAX_ITERATIONS)
        {
            Vector3 randPosition = randPosAroundObject(targetPos);

            var agent = controller.navAgent;
            var path = new NavMeshPath();
            agent.CalculatePath(randPosition, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    return randPosition;
                case NavMeshPathStatus.PathPartial:
                    break;
                default:
                    break;
            }

            ITERATIONS++;
        }

        return new Vector3(0, 0, 0);
    }

    public AiState_Roaming(int ID, AiController Controller, Targeter MoveTargeter, float MinDistance, float MaxDistance, float MinWait, float MaxWait) { 
        minDistanceToTarget = MinDistance;
        maxDistanceToTarget = MaxDistance;
        moveTargeter = MoveTargeter;
        controller = Controller;

        _stateID = ID; 
        minWaitTime = MinWait;
        maxWaitTime = MaxWait; 
    }

}
