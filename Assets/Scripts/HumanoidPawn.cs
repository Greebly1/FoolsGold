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
    bool sprinting = false;
    bool isSprinting
    {
        get { return currSpeed > 0.67f; }
    }

    bool crouched = false;

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

    protected override void UpdateAnimator()
    {
        base.UpdateAnimator(); //support base animation controller params from parent class

        //Humanoid skeletal mesh animation controllers supports crouching and sprinting input
        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
    }

}

