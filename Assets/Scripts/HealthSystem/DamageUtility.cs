using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

/// <summary>
/// Useful functions for working with a damage bitmask
/// </summary>
public class DamageUtility
{

    //true if damage contains the bitmask from searchtarget
    public static bool ContainsType(int damage, DamageType searchTarget)
    {
        return (damage & (int)searchTarget) == (int)searchTarget;
    }

    //Overload that takes array argument
    public static bool ContainsType(int damage, DamageType[] types)
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


    //-----ADDING-----
    #region Adding Functions
    //Adds a bitmask bit to an int if that int does not already have that bit active
    public static int AddDamageType(int a, DamageType b)
    {
        return a | (int)b;
    }
    
    //Overload that takes array argument
    public static int AddDamageType(int a, DamageType[] b)
    {
        int output = a;
        for (int count = 0; count < b.Length; count++)
        {
            output = output | (int)b[count];
        }

        return output;
    }
    
    //overload that takes two bitmask ints
    public static int AddDamageType(int a, int b)
    {
        return a | b;
    }
    #endregion

    //-----SUBTRACTING-----
    #region Subtracting Functions
    //bitwise negate from an int using an enum
    public static int SubtractDamageType (int a, DamageType b)
    {
        return a & ~(int)b;
    }
    //bitwise negate from an int from an array of enum
    public static int SubtractDamageType (int a, DamageType[] b)
    {
        int output = a;
        for (int count = 0; count < b.Length; count++)
        {
            output &= ~(int)b[count];
        }

        return output;
    }
    //bitwise negate two ints
    public static int SubtractDamageType(int a, int b)
    {
        return a & ~b;
    }
    #endregion

    //-----TOGGLE-----
    #region Toggle Functions
    //bitwise XOR
    public static int ToggleDamageType(int a, DamageType b)
    {
        return a ^ (int)b;
    }
    public static int ToggleDamageType(int a, DamageType[] b)
    {
        int output = a;
        for(int count = 0; count < b.Length; count++)
        {
            output ^= (int)b[count];
        }
        return output;
    }
    public static int ToggleDamageType(int a, int b)
    {
        return a ^ b;
    }
    #endregion

    //Returns true if the given damage bitmask contains a damage type bitflag that the resistance bitmask does not have set to true
    public static bool CompareResistanceToDamage(int resistance, int damage)
    {
        return (~resistance & damage) > 0;
    }
}

//Bitmask enum names
public enum DamageType
{
    none = 0, //0
    physical = 1 << 0, //1
    ethereal = 1 << 1, //2
    fire = 1 << 2, //4
    shock = 1 << 3, //8
    holy = 1 << 4, //16
    psychic = 1 << 5, //32
}

// 00000001 -> 1

//0000100 ->4