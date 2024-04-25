using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

[System.Serializable]
public class ResistancePowerup
{
    [DamageType] [SerializeField] int _powerup;
    [HideInInspector] public int powerup {  get { return _powerup; } }
    [SerializeField] public float duration = 5;
    [SerializeField] public GameObject visualEffectPrefab = null;

    [HideInInspector] public GameObject powerupVisualizer = null;

}

