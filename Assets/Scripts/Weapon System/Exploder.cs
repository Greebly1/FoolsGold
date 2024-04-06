using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//It EXPLODES
//TODO: make a visual
public class Exploder : MonoBehaviour
{
    public UnityEvent OnExplode;
    public float explosionRadius = 20;


    public void Explode(Damage damage)
    {
        Collider[] damageTargets = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider target in damageTargets)
        {
            try
            {
                Status damagable = target.GetComponent<Status>();
                damagable.Damage(damage);
            }
            catch { /*the thingamabob does not have a health component */ }

        }

        OnExplode.Invoke();
    }
}
