using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IHoldable
{
    [Header("Weapon Events")]
    public UnityEvent OnPrimaryAttack;
    public UnityEvent OffPrimaryAttack;
    public UnityEvent OnSecondaryAttack;
    public UnityEvent OffSecondaryAttack;


    #region Holdable interface
    [SerializeField] bool _isTwoHanded = false;
    [SerializeField] GameObject leftHandPosition = null;
    [SerializeField] GameObject rightHandPosition = null;
    bool IHoldable.isTwoHanded { get => _isTwoHanded;  set => _isTwoHanded = value;  }
    GameObject IHoldable.handPos_Left { get => leftHandPosition; set => leftHandPosition = value; }
    GameObject IHoldable.handPos_Right { get => leftHandPosition; set => leftHandPosition = value; }
    #endregion

    public void Action_Primary(bool initiate)
    {
        if (initiate) OnPrimaryAttack.Invoke();
        else OffPrimaryAttack.Invoke();
    }

    public void Action_Secondary(bool initiate)
    {
        if (initiate) OnSecondaryAttack.Invoke();
        else OffSecondaryAttack.Invoke();
    }


}
