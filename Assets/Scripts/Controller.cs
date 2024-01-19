using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn possessedPawn { get; private set; } = null;


    public void possessPawn (Pawn pawn)
    {
        possessedPawn = pawn;
    }
}