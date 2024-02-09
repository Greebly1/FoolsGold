using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon attachment that performs a bullet raycast
/// </summary>
public class WeaponAction_Raycast : WeaponAction //GameAction, Monobehavior
{
    #region vars

    #region inspector variables
    //specifies the origin of the bullet, and used for a forward vector for the raycast
    [SerializeField] Transform firePointTransform;
    #endregion

    #region Shorthand Getters
    Vector3 firePointPosition { get { return firePointTransform.position; } }
    Vector3 firePointForward { get { return firePointTransform.forward; } }

    #endregion

    #endregion

    #region Monobehavior Callbacks
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
}
    #endregion

    #region Weapon Event Responders
    //public void functions with no parameters to be subscribed to weapon events via the inspector
    public void triggerPulled()
    {

    }
    public void triggerReleased()
    {

    }


    #endregion
}
