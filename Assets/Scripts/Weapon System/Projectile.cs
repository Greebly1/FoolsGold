using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : IDamage
{
    [SerializeField] public float proj_Speed = 5;
    [SerializeField] public float proj_TimeToLive = 100;
    [SerializeField] public int proj_Damage = 10;
    [SerializeField] public float proj_radius = 10;
    
    [SerializeField] DamageEvent OnHit;
    [SerializeField] DamageEvent OnTimerEnd;

    float timeRemaining = 100;

    public bool isAlive
    {
        get { return timeRemaining >= 0; }
    }

    #region Monobehavior callbacks

    private void OnEnable()
    {
        timeRemaining = proj_TimeToLive;
        damage.amount = proj_Damage;
    }

    private void Awake()
    {
        damage = new Damage(proj_Damage, Team.noTeam);
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (isAlive == false)
        {
            //Debug.Log("Hit");
            OnTimerEnd?.Invoke(damage);
        }


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, proj_radius);
        if (hitColliders.Length > 0 ) { OnHit?.Invoke(damage); }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (transform.forward * proj_Speed * Time.fixedDeltaTime);
    }
    #endregion

}
