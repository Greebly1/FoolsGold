using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



/// <summary>
/// Base pawn defines a gameobject that has a movement component, and a health component
/// All pawns have a movement component (animator root motion) and a health component
/// 
/// For now all pawns will be player humanoid pawns
/// </summary>
public class HumanoidPawn : MonoBehaviour
{
    public Animator AnimationController { get; private set; }
    public CharacterStatus status { get; private set; }

    //Describes how this pawn is moving (this is the input for the AnimationController
    Vector2 movement = Vector2.zero;

    //Describes the goal movement of this pawn (this is the input the controller gives this pawn)
    Vector2 targetMovement = Vector2.zero;

    public Vector2 lastMovedDirection = new Vector2(0,1);


    [SerializeField] float turnSpeed = 720; //Degrees per second this pawn can rotate at
    float currSpeed
    {
        get { return movement.magnitude; }
    }
    float targetSpeed
    {
        get { return targetMovement.magnitude; }
    }

    Coroutine accelBlending;
    bool isAccelBlending = false;

    public float accelTime = 1.5f; //How many seconds it takes to accel from 0 to max speed

    bool sprinting = false;
    bool isSprinting
    {
        get { return currSpeed > 0.67f; }
    }

    float _maxSpeedPercent = 1f;
    float maxSpeedPercent
    {
        get { return _maxSpeedPercent; }
        set { _maxSpeedPercent = Mathf.Clamp(value, 0, 1);  }
    }
    public bool focusedOnTarget = false;
    bool crouched = false;

    [HideInInspector] public Vector3 lookTarget = Vector3.forward;

    #region Monobehavior Callbacks
    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    private void Update()
    {
        //Naive fix for issue when the focal target is basically inside the pawn
        //If the pawn is meant to be focusing on the target, and the target isn't too close to them
        if (focusedOnTarget && Vector3.Distance(new Vector3(lookTarget.x, 0, lookTarget.z), new Vector3(transform.position.x, 0, transform.position.z)) > 0.4)
        {
            float desiredYRot = Quaternion.LookRotation(new Vector3(lookTarget.x, 0, lookTarget.z) - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up).eulerAngles.y;

            Quaternion newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, desiredYRot, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 8 * Time.deltaTime);
        } else //Default to looking in the direction of the last movement
        {
            float desiredYRot = Quaternion.LookRotation(new Vector3(lastMovedDirection.x, 0, lastMovedDirection.y), Vector3.up).eulerAngles.y;
            Quaternion newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, desiredYRot, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 8 * Time.deltaTime);

        }

        UpdateAnimator();
    }
    #endregion
    public void run(Vector2 input)
    {
        targetMovement = Vector2.ClampMagnitude(input, 1);
        //If the coroutine isn't running, start the coroutine
        if (!isAccelBlending) { accelBlending = StartCoroutine("AccelerationBlending"); }

        UpdateAnimator();
    }

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

    private void UpdateAnimator()
    {
        Vector2 actualMovement;

        //The input must be rotated so it is no longer relative to the rotation of this pawn, instead it must be relative to the world
        actualMovement = rotateInput(Vector2.ClampMagnitude(movement, maxSpeedPercent));

        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);

        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
        //Debug.Log("updating animator with input: " + actualMovement);
    }


    #region Coroutines
    //Coroutine that smoothly changes the current movement vector towards the target movement vector
    IEnumerator AccelerationBlending()
    {
        isAccelBlending = true; //So outside scope knows if this coroutine is already running

        while (movement != targetMovement)
        {
            //unit vector pointing from _movement to _targetMovement
            Vector2 accelVec = (targetMovement - movement).normalized;
            //The new movement value, moves with a fixed speed
            Vector2 newMovement = movement + (accelVec * Time.deltaTime * (1 / accelTime));

            //If the distance from the new value is larger than the current value
            //This is in case it somehow overshoots the targetMovement
            if (Vector2.Distance(newMovement, targetMovement) > Vector2.Distance(movement, targetMovement))
            {
                movement = targetMovement;
            } else //This means the new movement is closer to the target movement
            {
                movement = newMovement;
            }

            yield return null;
        }

        isAccelBlending = false; //Tell outside scope that the coroutine is over
    }
    #endregion

    #region Helper Functions

    //Rotates an input vector by this pawn's y axis rotation
    public Vector2 rotateInput(Vector2 inputVec)
    {
        //Create a quaternion representing the rotation of this pawn only on the y up plane
        Quaternion pawnRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //Make a vector3 from the input param and multiply it by the quaternion, effectively rotating it
        Vector3 rotatedInput = Quaternion.Inverse(pawnRotation) * new Vector3(inputVec.x, 0, inputVec.y);

        //Return the vector 2 made by the y up plane of rotation
        return new Vector2(rotatedInput.x, rotatedInput.z);
    }
    #endregion
}

