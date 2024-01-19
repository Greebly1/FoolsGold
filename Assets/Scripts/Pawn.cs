using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



/// <summary>
/// Base pawn defines a gameobject that has a movement component, and a health component
/// All pawns have a movement component (animator root motion) and a health component
/// 
/// For now all pawns will be player humanoid pawns
/// </summary>
public class Pawn : MonoBehaviour
{
    public Animator AnimationController { get; private set; }
    public CharacterStatus status { get; private set; }

    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }
}
