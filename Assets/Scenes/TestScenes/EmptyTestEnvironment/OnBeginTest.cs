using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeginTest : MonoBehaviour
{
    [DamageType]
    public int damage = (1<<2) + (1<<4);

    [SerializeField]
    [DamageType]
    public int resistance = (1<<2) + (1<<4) + (1<<5);
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Damage: " + damage);
        Debug.Log("Resistance: " + resistance);

        if (DamageUtility.CompareResistanceToDamage(resistance, damage))
        {
            Debug.Log("TOOK DAMAGE");
        } else
        {
            Debug.Log("The damage was negated");
        }
        
    }

}
