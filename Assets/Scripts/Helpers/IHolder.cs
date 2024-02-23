using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface IHolder
{
    public GameObject leftHand {  get; }
    public GameObject rightHand { get; }

    public bool isHoldingObject { get; }

    public GameObject heldObjectEmptyTarget { get; }

    public void HoldObject(GameObject obj);

    public void StopHoldingObject();
}
