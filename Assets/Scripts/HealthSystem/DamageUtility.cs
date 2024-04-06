using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DamageUtility
{
    public static int bitmaskWidth
    {
        get
        {
            return sizeof(int);
        }
    }

    //true if damage contains the bitmask from searchtarget
    public static bool ContainsType(int damage, DamageType searchTarget)
    {
        if ((damage & (int)searchTarget) == (int)searchTarget)
        {
            return true;
        }

        return false;
    }

    //Overload that takes array argument
    public static bool ContainsTypes(int damage, DamageType[] types)
    {
        for (int count = 0; count < types.Length; count++)
        {
            if (!ContainsType(damage, types[count]))
            {
                return false;
            }
        }

        return true;
    }
}

//Bitmask enum names
public enum DamageType
{
    none = 0,
    physical = 1 << 0,
    ethereal = 1 << 1,
    fire = 1 << 2,
    shock = 1 << 3,
    holy = 1 << 4,
    psychic = 1 << 5,
    playerTeam = 1 << 6,
    enemyTeam = 1 << 7
}