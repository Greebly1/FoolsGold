using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public HumanoidPawn possessedPawn = null;


    public void possessPawn (HumanoidPawn pawn)
    {
        possessedPawn = pawn;
    }
}