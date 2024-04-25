using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectResistance : MonoBehaviour
{
    [SerializeField] ResistancePowerup powerup;

    public void ApplyEffect(GameObject target)
    {
        Status healthcompo = target.GetComponent<Status>();
        if (healthcompo != null)
        {
            healthcompo.AddPowerup(powerup);
        }
    }
}