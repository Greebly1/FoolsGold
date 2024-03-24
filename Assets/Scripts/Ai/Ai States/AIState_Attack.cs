using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Set the looktarget, then fire at the target if it is in line of sight
/// </summary>
public class AIState_Attack : IState
{
    AiController controller;
    AiSight senses;
    int _stateID;
    Targeter moveTargeter;
    public float minDistanceToTarget = 5;
    public float maxDistanceToTarget = 10;
    float minwait;
    float maxwait;
    float waitTimer;

    public int StateID()
    {
        return _stateID;
    }

    public void OnBegin()
    {
        controller.possessedPawn.lookTarget.setTarget(PlayerController.ClientPlayerController.possessedPawn);


        moveTargeter.transform.position = randPosAroundTarget();
    }

    public void OnEnd()
    {
        controller.possessedPawn.lookTarget.setFollow(false);
        controller.possessedPawn.lookAtTarget = false;
    }

    public void Tick()
    {
        //Shooting/sprinting logic
        if (senses.canSee(PlayerController.ClientPlayerController.possessedPawn))
        { //The ai sees the player
            (controller.possessedPawn as HumanoidPawn).PrimaryAction(true);
            controller.possessedPawn.lookAtTarget = true;
            if ((controller.possessedPawn as HumanoidPawn).Sprinting == true)
            {
                (controller.possessedPawn as HumanoidPawn).toggleSprint(false);
            }
            
        } else
        { //the ai does not see the player
            (controller.possessedPawn as HumanoidPawn).PrimaryAction(false);
            controller.possessedPawn.lookAtTarget = false;
            if ((controller.possessedPawn as HumanoidPawn).Sprinting == false)
            {
                (controller.possessedPawn as HumanoidPawn).toggleSprint(true);
            }

        }


        if (controller.isAtTarget)
        {
            //start a random wait timer
            moveTargeter.transform.position = randPosAroundTarget();
        }
    }



    public AIState_Attack(int ID, AiController Controller, AiSight Senses, Targeter moveTargeter, float MinDistance, float MaxDistance)
    {
        controller = Controller;
        _stateID = ID;
        this.moveTargeter = moveTargeter;
        this.minDistanceToTarget = MinDistance;
        this.maxDistanceToTarget = MaxDistance;
        senses = Senses;
    }

    Vector3 randPosAroundTarget()
    {
        while (true)
        {
            //Generate a random direction
            Vector2 dir = Vector2.zero.Randomize();

            float randDistance = Random.Range(minDistanceToTarget, maxDistanceToTarget);

            Vector3 randPosition = PlayerController.ClientPlayerController.possessedPawn.transform.position + (new Vector3(dir.x, 0, dir.y) * randDistance);

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
        }
        

        
    }
}
