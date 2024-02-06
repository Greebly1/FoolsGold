using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : Controller
{
    NavMeshAgent navAgent = null;

    //Controllers use themself as an empty targeting object
    Vector3 desiredPosition
    {
        get { return transform.position; }
    }
    Vector3 desiredVelocity = Vector3.zero;

    private void Awake()
    {
        if (possessedPawn == null ) { 
            Debug.LogWarning(this.name + " AI does not have a pawn"); 
            return; 
        }

        possessPawn(possessedPawn); //updates some getcomponent logic on startup, that logic resides in this function
        //I only want that logic to be in one place
    }

    public override void possessPawn (HumanoidPawn pawn)
    {
        possessedPawn = pawn;
        navAgent = possessedPawn.GetComponent<NavMeshAgent>();
        if (navAgent == null) {
            Debug.LogWarning(possessedPawn.name + " is possessed by " + this.name + " but does not contain a NavMeshAgent");
        } else
        {
            navAgent.updatePosition = false;
            navAgent.updateRotation = false;
        }
    }

    private void Update()
    {
        if (possessedPawn == null) { return; } //early out, AI can't do anything without a pawn

        navAgent.SetDestination(desiredPosition);
        desiredVelocity = navAgent.desiredVelocity;

        //The pawn expects a vector2 with max length of 1: constructing one using desiredVelocity
        Vector2 moveInput = Vector2.ClampMagnitude(new Vector2(desiredVelocity.x, desiredVelocity.z), 1);

        possessedPawn.setMoveVec(moveInput);
        //possessedPawn.lookTarget = desiredPosition; //this will probably cause errors

        navAgent.nextPosition = possessedPawn.transform.position;
        possessedPawn.lookTarget = possessedPawn.transform.position + Vector3.forward * 100;
    }
}
