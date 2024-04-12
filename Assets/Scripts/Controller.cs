using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn possessedPawn = null;
    public Team team = Team.noTeam;

    protected virtual void Awake ()
    {
        if (possessedPawn != null) { possessPawn(possessedPawn); }
    }

    protected virtual void possessPawn (Pawn pawn)
    {
        if (possessedPawn != null) { possessedPawn.ResetInput(); }
        possessedPawn = pawn;
        pawn.OnDeath.AddListener(OnPawnDeath);
        pawn.team = this.team;
    }

    protected virtual void unpossessPawn()
    {
        if (possessedPawn == null) { return; }

        possessedPawn.team = Team.noTeam;
        possessedPawn = null;
    }

    protected virtual void OnPawnDeath()
    {
        unpossessPawn();
    }

    public void tryPossessGameObject (GameObject possiblePawn)
    {
        Pawn tryGetPawn = possiblePawn.GetComponent<Pawn>();
        if (tryGetPawn != null) { possessPawn(tryGetPawn); }
    }
}