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
public class HumanoidPawn : MonoBehaviour
{
    public Animator AnimationController { get; private set; }
    public CharacterStatus status { get; private set; }

    //Describes how this pawn is moving (this is the input for the AnimationController
    Vector2 _movement = Vector2.zero;
    //Describes the goal movement of this pawn (this is the input the controller gives this pawn)
    Vector2 _targetMovement = Vector2.zero;

    [SerializeField] float turnSpeed = 720; //Degrees per second this pawn can rotate at
    float currSpeed
    {
        get { return _movement.magnitude; }
    }
    float targetSpeed
    {
        get { return _targetMovement.magnitude; }
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

    bool crouched = false;

    [HideInInspector] public Vector3 lookTarget = Vector3.forward;


    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    public void run(Vector2 input)
    {
        _targetMovement = Vector2.ClampMagnitude(input, 1);
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


        actualMovement = Vector2.ClampMagnitude(_movement, maxSpeedPercent);

        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);

        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
        //Debug.Log("updating animator with input: " + actualMovement);
    }

    private void Update()
    {
        //Naive fix for issue when the focal target is basically inside the pawn
        if (Vector3.Distance(new Vector3(lookTarget.x, 0, lookTarget.z), new Vector3(transform.position.x, 0, transform.position.z)) > 0.4)
        {

            float desiredYRot = Quaternion.LookRotation(new Vector3(lookTarget.x, 0, lookTarget.z) - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up).eulerAngles.y;

            Quaternion newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, desiredYRot, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 8*Time.deltaTime);
        }
    }

    //Coroutine that smoothly changes the current movement vector towards the target movement vector
    IEnumerator AccelerationBlending()
    {
        isAccelBlending = true; //So outside scope knows if this coroutine is already running

        while (_movement != _targetMovement)
        {
            //unit vector pointing from _movement to _targetMovement
            Vector2 accelVec = (_targetMovement - _movement).normalized;
            //The new movement value, moves with a fixed speed
            Vector2 newMovement = _movement + (accelVec * Time.deltaTime * (1 / accelTime));

            //If the distance from the new value is larger than the current value
            //This is in case it somehow overshoots the targetMovement
            if (Vector2.Distance(newMovement, _targetMovement) > Vector2.Distance(_movement, _targetMovement))
            {
                _movement = _targetMovement;
            } else //This means the new movement is closer to the target movement
            {
                _movement = newMovement;
            }

            UpdateAnimator();

            yield return null;
        }

        UpdateAnimator();

        isAccelBlending = false; //Tell outside scope that the coroutine is over
    }
}

