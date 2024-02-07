using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn possessedPawn = null;

    protected virtual void Awake ()
    {

    }

    protected virtual void possessPawn (Pawn pawn)
    {
        possessedPawn = pawn;
    }
}