using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

/// <summary>
/// Extensible & reusable state machine object from Jason Weimann on youtube
/// </summary>
public class StateMachine
{
    IState _currState = null;
    Dictionary<int, List<Transition>> transitions = new Dictionary<int, List<Transition>>(); //Each state has its own list of transitions out of that state
    List<Transition> transitions_Local {
        get
        {
            if (!transitions.ContainsKey(currState.StateID()))
            {
                transitions[currState.StateID()] = new List<Transition>();
            }
            return transitions[currState.StateID()];
        }
    } //Gets the current state's transitions out of the dictionary above

    List<Transition> transitions_Global = transitions_Empty; //special list of transitions that are checked regardless of what the current state is

    static List<Transition> transitions_Empty = new List<Transition>(capacity: 0); //Empty transition list used for code safety

    float _timeInState;
    public float timeInState
    {
        get { return _timeInState; }
    }

    /// <summary>
    /// currState property so that
    /// changing the current state calls the 
    /// IState.OnEnd() & IState.OnBegin() functions
    /// </summary>
    public IState currState
    {
        get => _currState;
        set
        {
            //Debug.Log("setting state to " + value.GetType().Name);
            if (currState == value || value == null) return; //don't do anything unless the new state is a different state

            currState?.OnEnd(); //End the current state
            _currState = value; //Swap to the new state
            currState.OnBegin(); //Start the new state
            _timeInState = 0;
        }
    }

    /// <summary>
    /// Executed on update, check for transitions, then do the current state's work
    /// </summary>
    public void Tick()
    {
        //Debug.Log("State machine in " + _currentState.stateName());

        Transition nextTransition = GetTransition(); //try to get a new transition
        if (nextTransition != null) //set the new state if we found a transition
            currState = nextTransition.nextState;

        currState?.Tick(); //Do the currentState's work
        _timeInState += Time.deltaTime;
    }

    /// <summary>
    /// Prioritizes transitions_Global, then transitions_Local
    /// Null if there are no available transitions
    /// </summary>
    /// <returns>The first available transition whoose condition is true</returns>
    private Transition GetTransition()
    {
        try
        {
            //check each _anyTransition for a true transition condition
            foreach (var transition in transitions_Global)
                if (transition.Condition())
                    return transition;

            //then check the _currentTransitions conditions
            foreach (var transition in transitions_Local)
                if (transition.Condition())
                    return transition;
        } catch (NullReferenceException ex) {
            Debug.LogWarning(ex.Message + " | A state machine does not have any transitions");
        }
        


        return null; //No available transition condition is true
    }

    /// <summary>
    /// Add a local transition
    /// </summary>
    public void AddTransitionLocal(IState FromState, IState ToState, Func<bool> When)
    {

        //Debug.Log("Added a transition from the " + FromState.GetType().Name + " To the " + ToState.GetType().Name);
        //create an empty list of transitions if this state has no transition list
        if (!transitions.ContainsKey(FromState.StateID()))
        {
            transitions[FromState.StateID()] = new List<Transition>();
        }

        Transition newTransition = new Transition(When, ToState);

        transitions[FromState.StateID()].Add(newTransition);
    }

    /// <summary>
    /// Add a transition to all states for this state machine
    /// </summary>
    /// <param name="to">The state to transition to</param>
    /// <param name="condition">True when the transition should happen</param>
    public void AddTransitionGlobal(IState NewState, Func<bool> When)
    {
        transitions_Global.Add(new Transition(When, NewState));
    }

}

//This is a class (must be a class to be nullable)
/// <summary>
/// Encapsulates a state to go to, with a boolean condition
/// </summary>
public class Transition
{
    public Func<bool> Condition { get; }
    public IState nextState { get; }

    public Transition (Func<bool> condition, IState NextState)
    {
        Condition = condition;
        nextState = NextState;
    }
}