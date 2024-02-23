using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_Projectile : WeaponAction
{
    [SerializeField] Transform firePointTransform;
    [SerializeField] int damage = 5; //TODO: encapsulate damage data into its own serializable struct
    [SerializeField] GameObject prefab = null;

    #region Shorthand Getters
    Vector3 firePointPosition { get { return firePointTransform.position; } }
    Vector3 firePointForward { get { return firePointTransform.forward; } }

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
        //Debug.Log("trigger pulled event handling");
        shoot();
    }
    public void triggerReleased()
    {

    }

    void shoot()
    {
        Debug.Log("Shooting projectile");

        //Locate the projectile object pool
        //Spawn the projectile from the object pool
        GameObjectPool.pools[prefab].PoolInstantiate(firePointPosition, firePointTransform.rotation);
    }
    #endregion
}
