using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Coroutines
{
    public delegate void voidFunc();
    /// <summary>
    /// Does 'thingToDo' after the delay in seconds
    /// </summary>
    public static IEnumerator DoAfterSeconds(float delay, voidFunc thingToDo)
    {
        float currDuration = 0;
        while (currDuration < delay)
        {
            currDuration += Time.deltaTime;
            yield return null;
        }
        thingToDo();
    }
    /// <summary>
    /// Does 'thingToDo' while condition evaluates to true
    /// </summary>
    public static IEnumerator DoWhileTrue(voidFunc thingToDo, Func<bool> condition)
    {
        while (condition())
        {
            thingToDo();
            yield return null;
        }
    }

    /// <summary>
    /// Does 'thingToDo' while the condition is true, then does 'exitAction' when condition evaluates to false
    /// </summary>
    public static IEnumerator DoWhileTrueWithExit(voidFunc thingToDo, voidFunc exitAction, Func<bool> condition)
    {
        while (condition())
        {
            thingToDo();
            yield return null;
        }
        exitAction();
    }

    /// <summary>
    /// Does 'thingToDo' after delay in seconds, unless condition evaluates to false
    /// </summary>
    public static IEnumerator DoAfterSecondsWhileTrue(float delay, voidFunc thingToDo, Func<bool> condition)
    {
        float currDuration = 0;

        while (condition())
        {
            if (currDuration < delay) { 
                thingToDo();
                break;
            }
            currDuration += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Does 'thingToDo' after delay in seconds, unless condition evaluates to false, whereupon it will execute the 'falseAction'
    /// </summary>
    public static IEnumerator DoAfterSecondsWhileTrueWithExit(float delay, voidFunc thingToDo, voidFunc falseAction, Func<bool> condition)
    {
        float currDuration = 0;

        while (condition())
        {
            if (currDuration < delay)
            {
                thingToDo();
                yield break;
            }
            currDuration += Time.deltaTime;
            yield return null;
        }

        falseAction();
    }

    
}
