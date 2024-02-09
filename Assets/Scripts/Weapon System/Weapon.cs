using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Events")]
    public UnityEvent OnPrimaryAttack;
    public UnityEvent OffPrimaryAttack;
    public UnityEvent OnSecondaryAttack;
    public UnityEvent OffSecondaryAttack;
}
