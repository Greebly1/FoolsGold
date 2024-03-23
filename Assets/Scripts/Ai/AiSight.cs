using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles AI Sight
/// </summary>
public class AiSight : MonoBehaviour
{
    public GameObjectEvent SeenNewTarget;
    public GameObjectEvent LostATarget;

    List<GameObject> _seenTargets = new List<GameObject>();
    public List<GameObject> seentargets { 
        get => _seenTargets;
        private set {  _seenTargets = value; } 
    }

    public float fieldOfView = 50.0f;
    public float sightRange = 100.0f;

    public List<GameObject> trackedTargets = new List<GameObject>(); //Dependency inject the targets it looks for


    #region sight functions
    public bool canSee(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float targetAngle = Vector3.Angle(transform.forward, targetDirection);
        if (targetAngle <= fieldOfView && Vector3.Distance(this.transform.position, target.transform.position) <= sightRange)
        {
            //Debug.Log("target within field of view");
            RaycastHit hitinfo;
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hitinfo) && hitinfo.collider.gameObject == target)
            {
                //Debug.Log("sees the target");
                return true;
            }
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

        foreach (GameObject seenObject in seentargets)
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

        if (targetsSeenThisFrame.Count > seentargets.Count) //we have seen one or more new targets
        {
            //Find the new target(s)
            List<GameObject> newTargets = targetsSeenThisFrame
                .Where<GameObject>(target => !seentargets.Contains(target))
                .ToList();

            //Invoke the seen target event for each target
            foreach (GameObject newTarget in newTargets)
            {
                SeenNewTarget.Invoke(newTarget);
            }
            
        } else if (targetsSeenThisFrame.Count < seentargets.Count) //we have lost sight of one or more targets
        {
            //Find the lost target(s)
            List<GameObject> lostTargets = seentargets
                .Where<GameObject>(target => !targetsSeenThisFrame.Contains(target))
                .ToList();

            //Invoke the lost target event for each target
            foreach (GameObject newTarget in lostTargets)
            {
                LostATarget.Invoke(newTarget);
            }
        }

        seentargets = targetsSeenThisFrame;
    }

    #endregion
}
