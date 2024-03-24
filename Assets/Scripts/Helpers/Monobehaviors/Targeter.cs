using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    protected Transform target = null; //the gameobject this targeter is following

    bool followTarget = false; //whether to follow the target or not

    Coroutine followCoroutine = null;
    bool isFollowingTarget = false; //if coroutine is running or not

    public void setTarget(MonoBehaviour newTarget, bool follow = true)
    {
        setTarget(newTarget.gameObject.transform, follow);
    }

    public void setTarget(GameObject newTarget, bool follow = true)
    {
        setTarget(newTarget.transform, follow);
    }

    public void setTarget(Transform newTarget, bool follow = true)
    {
        target = newTarget; //do nullchecks outside of this script
        setFollow(follow);
    }

    public void setFollow(bool follow)
    {
        if (followTarget == follow) return; //Early out

        if (follow == true) //The follow must be true, and the followtarget must be false
        {
            followTarget = true;
            followCoroutine = StartCoroutine("followObject"); ;
        } else
        {
            followTarget = false;
        }
    }
    #region coroutines

    IEnumerator followObject()
    {
        isFollowingTarget = true;
        while (followTarget && target != null)
        {
            DoFollow();
            
            yield return null;
        }

        isFollowingTarget = false;
    }

    protected virtual void DoFollow()
    {
        transform.position = target.transform.position;
    }

    #endregion
}
