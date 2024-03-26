using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Handles AI Sight, unfortunately I didnt comment most of this when I made it and forgot how it works
/// I had plans to make it much more robust so it automatically sends updates when it sees interesting things such as the player, a teammate or an item
/// however, as of commit <cad94328dbe5f3ae7f58058c79ff5489565b7ff0> I only use the CanSee(gameobject) function
/// </summary>
public class AiSight : MonoBehaviour
{
    public GameObjectEvent SeenNewTarget;
    public GameObjectEvent LostATarget;

    List<GameObject> _seenTargets = new List<GameObject>();
    public List<GameObject> targetsSeenLastFrame { 
        get => _seenTargets;
        private set {  _seenTargets = value; } 
    }

    public float fieldOfView = 50.0f;
    public float sightRange = 100.0f;

    public List<GameObject> trackedTargets = new List<GameObject>(); //Dependency inject the targets it looks for


    #region sight functions
    public bool canSee(MonoBehaviour target)
    {
        if (target == null) return false;

        return canSee(target.gameObject);
    }
    public bool canSee(GameObject target)
    {
        if (target == null) return false;

        Vector3 targetDirection = (target.transform.position - this.transform.position).normalized;
        
        RaycastHit hitinfo;
        Ray targetRay = new Ray(transform.position, targetDirection);

        if (Physics.Raycast(targetRay, out hitinfo, sightRange))
        {
            if (hitinfo.collider.gameObject == target) { return true; }
        }



        return false;
    }

    public bool seesType<type>()
    {
        List<type> types = new List<type>();
        return seesType<type>(out types);
    }

    public bool seesType<type>(out List<type> seenList)
    {
        seenList = new List<type>();

        foreach (GameObject seenObject in targetsSeenLastFrame)
        {
            type typeCast = seenObject.GetComponent<type>();

            if (typeCast != null)
            {
                seenList.Add(typeCast);
            }
        }

        if (seenList.Count > 0) { return true; }

        return false;
    }
    #endregion


    #region Monobehavior Callbacks

    void Update()
    {
        List<GameObject> targetsSeenThisFrame = trackedTargets //A list of tracked targets that we can see
            .Where<GameObject>(target => target != null && canSee(target))
            .ToList();

        if (targetsSeenThisFrame.Count > targetsSeenLastFrame.Count) //we have seen one or more new targets
        {
            //Find the new target(s)
            List<GameObject> newTargets = targetsSeenThisFrame
                .Where<GameObject>(target => !targetsSeenLastFrame.Contains(target))
                .ToList();

            //Invoke the seen target event for each target
            foreach (GameObject newTarget in newTargets)
            {
                SeenNewTarget.Invoke(newTarget);
                Debug.Log("Seen new target");
            }
            
        } else if (targetsSeenThisFrame.Count < targetsSeenLastFrame.Count) //we have lost sight of one or more targets
        {
            //Find the lost target(s)
            List<GameObject> lostTargets = targetsSeenLastFrame
                .Where<GameObject>(target => !targetsSeenThisFrame.Contains(target))
                .ToList();

            //Invoke the lost target event for each target
            foreach (GameObject newTarget in lostTargets)
            {
                LostATarget.Invoke(newTarget); 
                Debug.Log("Lost sight of a target");
            }
        }

        targetsSeenLastFrame = targetsSeenThisFrame;
    }

    #endregion
}
