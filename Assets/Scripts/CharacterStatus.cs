using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// The character status object (should be a scriptable object)
/// Encapsulates:
/// - Health, stamina, focus points, status effects, etc as well as functions for interfacing with data
/// (Note: status effects are just bools for this and outside objects to know what is active, the actual effect is handled by a coroutine)
/// This object also contains a heap structure for storing status effects
/// 
/// TODO
/// Implement resistances, status buildups
/// </summary>
public class CharacterStatus : MonoBehaviour
{
    public MetaVal<float> Health { get; private set; }

    public bool isDead
    {
        get
        {
            return Health.value <= 0;
        }
    }

    private void Start()
    {
        Health.valueChanged += checkHealth;
    }

    private void checkHealth(int delta, float newHealth)
    {

    }

    private void die()
    {
        Debug.Log(this + " has died");
    }
}

/// <summary>
/// All possible status effects
/// 
/// TODO:
/// Add a status effect manager singleton and move this into that script
/// Add more status effects
/// </summary>
public enum statusEffects
{
    Bleeding,
    Regeneration
}
