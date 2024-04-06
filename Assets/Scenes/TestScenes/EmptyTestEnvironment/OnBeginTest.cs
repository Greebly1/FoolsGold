using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeginTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int damage = 17;
        Debug.Log(DamageUtility.ContainsType(damage, DamageType.ethereal));
        Debug.Log(DamageUtility.ContainsType(damage, DamageType.holy));
    }

}
