using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float proj_Speed = 5;
    [SerializeField] public float proj_TimeToLive = 100;
    [SerializeField] public int proj_Damage = 10;
    [SerializeField] public float proj_radius = 10;

    [SerializeField] IntEvent OnHit;
    [SerializeField] IntEvent OnTimerEnd;

    float timeRemaining = 100;

    public bool isAlive
    {
        get { return timeRemaining >= 0; }
    }

    #region Monobehavior callbacks

    private void OnEnable()
    {
        timeRemaining = proj_TimeToLive;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (isAlive == false)
        {
            Debug.Log("Hit");
            OnTimerEnd.Invoke(proj_Damage);
        }


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, proj_radius);
        if (hitColliders.Length > 0 ) { OnHit.Invoke(proj_Damage); }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (transform.forward * proj_Speed * Time.fixedDeltaTime);
    }
    #endregion

}
