using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Pickup : MonoBehaviour, IInteractable
{
    public GameObjectEvent OnPickup;

    public void interact(GameObject instigator)
    {
        ApplyEffect(instigator);
        OnPickup.Invoke(instigator);
    }

    protected virtual void ApplyEffect(GameObject effectTarget)
    {
        
    }

    public void debuggMessage()
    {
        //Debug.Log("The pickup worked");
    }
}
