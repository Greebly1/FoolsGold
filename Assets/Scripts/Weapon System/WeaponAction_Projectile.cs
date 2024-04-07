using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAction_Projectile : WeaponAction
{
    [SerializeField] Transform firePointTransform;
    [SerializeField] Damage damage; //TODO: encapsulate damage data into its own serializable struct
    [SerializeField] GameObject prefab = null;
    bool triggerHeld = false;
    [SerializeField] float autofireDelay = 0.1f;
    Coroutine autofireRoutine;
    float autofireTimer = 0;

    public Weapon owningWeapon = null;

    public UnityEvent fireEvent;
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
        autofireTimer -= Time.deltaTime;
    }
    #endregion




    #region Weapon Event Responders

    public void onPickup()
    {
        //We need to update the team enum inside the damage struct
        damage.teamSource = owningWeapon.owningTeam;
    }

    //public void functions with no parameters to be subscribed to weapon events via the inspector
    public void triggerPulled()
    {
        //Debug.Log("trigger pulled event handling");
        triggerHeld = true;
        autofireRoutine = StartCoroutine("autofire");
    }
    public void triggerReleased()
    {
        triggerHeld = false;
    }

    void shoot()
    {
        autofireTimer = autofireDelay;
        fireEvent.Invoke();
        //Locate the projectile object pool
        //Spawn the projectile from the object pool
        GameObjectPool.pools[prefab]
            .PoolInstantiate(firePointPosition, firePointTransform.rotation)
            .GetComponent<IDamage>()
            .damage = this.damage;
    }
    #endregion

    IEnumerator autofire()
    {
        while (triggerHeld)
        {
            if (autofireTimer <= 0)
            {
                shoot();

            }
            
            yield return null;
        }
    }
}
