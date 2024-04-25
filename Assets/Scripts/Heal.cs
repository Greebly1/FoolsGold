using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] int healAmount = 5;

    public void HealTarget(GameObject target)
    {
        Status healthcompo = target.GetComponent<Status>();
        if (healthcompo != null)
        {
            healthcompo.Heal(healAmount);
        }
    }
}
