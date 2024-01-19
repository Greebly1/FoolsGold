using System;
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
    public event Action death;
    public event Action<float> healthChanged;
    [SerializeField] float _currHealth;
    public float currHealth { 
        get { return _currHealth; } 
        private set { 
            if (value != _currHealth) { 
                _currHealth = value; 
                healthChanged.Invoke(value);
            } } }

    public bool isDead { get { return _currHealth <= 0; } }

    private void Awake()
    {
        healthChanged += checkDeath;
    }

    public void checkDeath(float health)
    {
        checkDeath();
    }
    public void checkDeath ()
    {
        if (isDead)
        {
            healthChanged -= checkDeath;
            death?.Invoke(); 
        }
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
