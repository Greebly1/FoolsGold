using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Roamer : AiController
{
    IState RoamingState;
    IState IdleState;

    [SerializeField] float minWaitTime = 0;
    [SerializeField] float maxWaitTime = 10;
    float randomWaitTime;

    protected override void Awake()
    {
        base.Awake();

        RoamingState = new AiState_Roaming(1, this, moveTargeter, 5, 10);
        IdleState = new AiState_Idle(2);

        decisionStateMachine.AddTransitionLocal(RoamingState, IdleState, atTarget);
        decisionStateMachine.AddTransitionLocal(IdleState, RoamingState, waitTimer);

        decisionStateMachine.currState = IdleState;

    }

    protected override void Update()
    {
        base.Update();
    }

    bool atTarget()
    {
        return isAtTarget;
    }

    bool waitTimer()
    {
        //generate a random time
        randomWaitTime = 5;

        return decisionStateMachine.timeInState >= randomWaitTime;
    }
}
