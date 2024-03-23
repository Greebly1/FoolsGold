using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiController : Controller
{
    protected NavMeshAgent navAgent = null;
    [SerializeField] protected Targeter moveTargeter;
    [SerializeField] protected AiSight senses;

    [Tooltip("The minimum distance the pawn can be from a target, for the controller to consider the pawn to be 'at the target'")]
    [SerializeField] protected float moveToPrecision = 1;

    protected StateMachine decisionStateMachine;
    public GameObject selectedTarget;

    Vector3 desiredPosition
    {
        get { return moveTargeter.transform.position; }
    }
    Vector3 desiredVelocity = Vector3.zero;

    public bool isAtTarget
    {
        get
        {
            return Vector3.Distance(possessedPawn.transform.position, desiredPosition) <= moveToPrecision;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (possessedPawn == null ) { 
            Debug.LogWarning(this.name + " AI does not have a pawn"); 
            return; 
        }

        decisionStateMachine = new StateMachine();

        possessPawn(possessedPawn); //updates some getcomponent logic on startup, that logic resides in this function
        //I only want that logic to be in one place
    }

    protected override void possessPawn (Pawn pawn)
    {
        base.possessPawn(pawn);

        navAgent = possessedPawn.GetOrAddComponent<NavMeshAgent>();

        navAgent.updatePosition = false;
        navAgent.updateRotation = false;

        ICamTargetable SensesOrigin = possessedPawn.GetComponent<ICamTargetable>();
        TargeterWithRot AiSenseTargeter = senses.GetOrAddComponent<TargeterWithRot>();

        if (SensesOrigin != null) { AiSenseTargeter.setTarget(SensesOrigin.CamTransform().transform); }
        else {  AiSenseTargeter.setTarget(possessedPawn.transform); }

        moveTargeter.transform.position = possessedPawn.transform.position;
    }

    protected virtual void Update()
    {
        if (possessedPawn == null) { return; } //early out, AI can't do anything without a pawn

        if (!isAtTarget)
        {
            moveLogic();
        } else
        {
            possessedPawn.setMoveVec(Vector2.zero);
        }

        decisionStateMachine.Tick();

    }

    protected void moveLogic()
    {
        navAgent.SetDestination(desiredPosition);
        desiredVelocity = navAgent.desiredVelocity;

        //The pawn expects a vector2 with max length of 1: constructing one using desiredVelocity
        Vector2 moveInput = Vector2.ClampMagnitude(new Vector2(desiredVelocity.x, desiredVelocity.z), 1);

        possessedPawn.setMoveVec(moveInput);
        //possessedPawn.lookTarget = desiredPosition; //this will probably cause errors

        navAgent.nextPosition = possessedPawn.transform.position;
        possessedPawn.lookTarget.transform.position = possessedPawn.transform.position + Vector3.forward * 100;
    }
}
