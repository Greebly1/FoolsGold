using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Encapsulates a team, and a damage amount
public struct Damage
{
    public int amount;
    public Team teamSource;

    public Damage (int Amount, Team team)
    {
        amount = Amount;
        teamSource = team;
    }
}

public enum Team
{
    player,
    enemy,
    noTeam
}

public abstract class IDamage : MonoBehaviour
{
    public Damage damage;
}