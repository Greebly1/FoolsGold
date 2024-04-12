using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Ragdoll : Death_Delete
{
    [SerializeField] bool destroyAfterDelay = true;

    protected override IEnumerator killCoroutine()
    {
        //first enable ragdoll physics
        IRagdoll ragdollAblePawn = GetComponent<Pawn>() as IRagdoll;
        if (ragdollAblePawn != null )
        {
            //we have found a valid ragdoll
            ragdollAblePawn.isRagdoll = true;
        }

        //then do the Death_Delete delay coroutine
        if ( destroyAfterDelay )
        {
            yield return base.killCoroutine();
        } else
        {
            yield return null;
        }
    }
}
