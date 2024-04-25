using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Grunt : AiController
{
    IState attackState;
    IState idleState;
    IState roamState;


    [SerializeField] float idealRange = 10;
    protected override void Awake()
    {
        base.Awake();

        idleState = new AiState_Idle(1);
        attackState = new AIState_Attack(2, this, senses, moveTargeter, 3, 6);
        roamState = new AiState_Roaming(3, this, moveTargeter, 10, 30, 6, 9);

        decisionStateMachine.AddTransitionLocal(roamState, attackState, playerInLineOfSight);

        decisionStateMachine.currState = roamState;

    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log(playerInLineOfSight());
    }

    bool atTarget()
    {
        return isAtTarget;
    }

}
