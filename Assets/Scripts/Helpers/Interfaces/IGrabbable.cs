using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IHoldable
{
    public bool isTwoHanded { get; protected set; }
    public GameObject handPos_Left { get; protected set; }
    public GameObject handPos_Right { get; protected set; }
    public void setTeam(Team newTeam);


    public void Pickup(GameObject newOwner);
}

