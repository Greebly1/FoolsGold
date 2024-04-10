using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IRagdoll
{


    public bool isRagdoll { get; set; }

}

/// <summary>
/// I am using this to define default functionlity for the Iragdoll
/// </summary>
public class RagdollUtility
{

    //Disable colliders and gravity of rootbone, and in child gameobjects of rootbone
    public static void DisableRigPhysics(GameObject rootBone)
    {
        if (rootBone == null) //centralize null checks to the utility function
        {
            Debug.LogError("ragdoll utility was passed a null root bone");
            return;
        }

        Rigidbody[] rigRigidbodies = rootBone.GetComponentsInChildren<Rigidbody>();
        Collider[] rigColliders = rootBone.GetComponentsInChildren<Collider>();

        foreach (Rigidbody physicsbody in rigRigidbodies)
        {
            //physicsbody.isKinematic = false;
            physicsbody.useGravity = false;
        }

        foreach (Collider physicsbody in rigColliders)
        {
            physicsbody.enabled = false;
        }
    }

    //enable colliders and gravity of rootbone, and in child gameobjects of rootbone
    public static void EnableRigPhysics(GameObject rootBone)
    {
        if (rootBone == null) //centralize null checks to the utility function
        {
            Debug.LogError("ragdoll utility was passed a null root bone");
            return;
        }

        Rigidbody[] rigRigidbodies = rootBone.GetComponentsInChildren<Rigidbody>();
        Collider[] rigColliders = rootBone.GetComponentsInChildren<Collider>();

        foreach (Rigidbody physicsbody in rigRigidbodies)
        {
            //physicsbody.isKinematic = true;
            physicsbody.useGravity = true;
        }

        foreach (Collider physicsbody in rigColliders)
        {
            physicsbody.enabled = true;
        }
    }

}