using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



/// <summary>
/// The 
/// </summary>
public class HumanoidPawn : Pawn
{
    #region vars

    [SerializeField] GameObject heldObject = null;

    IHoldable heldItem { get { return heldObject.GetComponentInChildren<IHoldable>(); } }

    bool sprinting = false;
    bool isSprinting
    {
        get { return currSpeed > 0.67f; }
    }

    bool crouched = false;
    #endregion

    #region Input Functions
    public void toggleSprint(bool isButtonHeld)
    {
        if (!isButtonHeld && crouched) { 
            toggleCrouch();
            return; } // early out

        if (!isButtonHeld && !sprinting)
        {
            return; // early out
        }

        sprinting = !sprinting;
        //Debug.Log("Sprinting is :" + sprinting + ", button is held is: " + isButtonHeld);
        //if (!isSprintBlending) { sprintBlending = StartCoroutine("SprintToggleTimer"); }
        if (sprinting && crouched) { crouched = false; }

        UpdateAnimator();
    }

    public void toggleCrouch()
    {
        crouched = !crouched;
        sprinting = false;

        UpdateAnimator();
    }

    //TODO: extract some attack logic into the base pawn class so nonhumanoid pawns can still attack
    public void PrimaryAction(bool isInitiating)
    {
        try
        {
            (heldItem as Weapon)?.Action_Primary(isInitiating);
        } catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
            //TODO: make default attack functionality for one handed or two handed items, and default unarmed attack functionality
        }
    }

    public void SecondaryAction(bool isInitiating)
    {
        try
        {
            (heldItem as Weapon)?.Action_Secondary(isInitiating);
        }
        catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
            //TODO: make default attack functionality for one handed or two handed items, and default unarmed attack functionality
        }
    }

    public void FocusItem(bool isInitiating)
    {
        try
        {
            //depending on isInitiating

            //play an animation to set up the aim animation layers
            //play an animation to raise the weapon
        }
        catch
        {
            //we either 1: are not holding an item or
            //          2: the item we are holding is not a weapon
        }
    }

    #endregion

    #region Pawn class overrides
    protected override void UpdateAnimator()
    {
        base.UpdateAnimator(); //support base animation controller params from parent class

        //Humanoid skeletal mesh animation controllers supports crouching and sprinting input
        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
        //Debug.Log(lookAtTarget);
        AnimationController.SetBool("Aiming", lookAtTarget);
    }
    #endregion
}

