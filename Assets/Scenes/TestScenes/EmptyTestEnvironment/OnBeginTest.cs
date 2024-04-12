using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeginTest : MonoBehaviour
{
    [DamageType]
    public int damage;

    [SerializeField]
    [DamageType]
    public int resistance;
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
