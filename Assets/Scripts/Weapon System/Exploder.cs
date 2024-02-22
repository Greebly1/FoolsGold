using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//It EXPLODES
//TODO: make a visual
public class Exploder : MonoBehaviour
{
    public IntEvent OnExplode;
    public float explosionRadius = 20;

    public void Explode(int damage)
    {
        Collider[] damageTargets = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider target in damageTargets)
        {
            try
            {
                HealthStatus damagable = target.GetComponent<HealthStatus>();
                damagable.Damage(damage);
            } catch { /*the thingamabob does not have a health component */ }
            
        }

        OnExplode.Invoke(damage);
    }
}
