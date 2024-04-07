using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Encapsulates a team, and a damage amount
[System.Serializable]
public struct Damage
{
    public int amount;
    [DamageType]
    public int damagetype;
    public Team teamSource;

    public Damage (int Amount, Team team = Team.noTeam, int DamageType = 1)
    {
        amount = Amount;
        teamSource = team;
        damagetype = DamageType;
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

public class DamageTypeAttribute : PropertyAttribute
{

}