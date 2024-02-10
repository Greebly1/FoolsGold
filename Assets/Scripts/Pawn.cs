using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The base class for the pawn component
/// The pawn will be the central hub for interfacing with a character gameobject. It contains input values and events
/// Controllers possess pawns and update these input values/events
/// The pawn then sends these values out to their components
/// Movement is tightly coupled to the concrete implementation of pawn, due to the use of root motion
/// </summary>
[RequireComponent(typeof(Animator))]
public class Pawn : MonoBehaviour
{
    #region Vars
    [SerializeField] public Targeter lookTarget; //Gameobject that this pawn can use to look at a position
    [SerializeField] float turnSpeed = 720; //Degrees per second this pawn can rotate at
    [SerializeField] float accelTime = 1.5f; //How many seconds it takes to accel from 0 to max speed, Higher values will feel clunkier, shorter values will feel snappier
    [HideInInspector] public bool lookAtTarget = false;

    #region Components
    protected Animator AnimationController { get; private set; }
    #endregion

    public Vector2 inputMovement = Vector3.zero;  //Describes the goal movement of this pawn (this is the input the controller gives this pawn)
    protected Vector2 currMovement = Vector3.zero; //Describes how this pawn is moving (this is the input for the AnimationController
    public Vector2 lastMovedDirection = new Vector2(0, 1); //used for setting the pawn orientation when lookAtTarget == false;

    float _maxSpeedPercent = 1f; //controls the max speed of this pawn used to clamp input going into the animation controller
    float maxSpeedPercent
    {
        get { return _maxSpeedPercent; }
        set { _maxSpeedPercent = Mathf.Clamp(value, 0, 1); }
    }
    

    #region Coroutine Vars
    Coroutine accelBlending;
    bool isAccelBlending = false;
    #endregion

    #region Shorthand Getters
    //These getters will help improve readability of this script
    protected float lookTargetX { get { return lookTarget.transform.position.x; } }
    protected float lookTargetY { get {  return lookTarget.transform.position.y; } }
    protected float lookTargetZ { get {  return lookTarget.transform.position.z; } }

    protected float currSpeed
    {
        get { return currMovement.magnitude; }
    }
    protected float inputSpeed
    {
        get { return inputMovement.magnitude; }
    }
    #endregion

    #endregion

    #region Monobehavior Callbacks

    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
    }

    private void Update()
    {
        //Naive fix for issue when the focal target is basically inside the pawn
        //If the pawn is meant to be focusing on the target, and the target isn't too close to them
        if (lookAtTarget && Vector3.Distance(new Vector3(lookTargetX, 0, lookTargetZ), new Vector3(transform.position.x, 0, transform.position.z)) > 0.4)
        {
            Vector3 lookDirection = new Vector3(lookTargetX, 0, lookTargetZ) - new Vector3(transform.position.x, 0, transform.position.z);
            slerpYRot(lookDirection);
        }
        else //Default to looking in the direction of the last movement
        {
            slerpYRot(lastMovedDirection);
        }

        UpdateAnimator();
    }

    #endregion

    #region Input Functions
    public void setMoveVec(Vector2 input)
    {
        inputMovement = Vector2.ClampMagnitude(input, 1);
        //If the coroutine isn't running, start the coroutine
        if (!isAccelBlending) { accelBlending = StartCoroutine("AccelerationBlending"); }
    }
    #endregion

    protected virtual void UpdateAnimator()
    {
        Vector2 actualMovement = Vector2.ClampMagnitude(inputMovement, maxSpeedPercent); 

        //The input must be rotated so it is no longer relative to the rotation of this pawn, instead it must be relative to the world
        actualMovement = this.gameObject.rotateInput(actualMovement);

        //All animation controllers can move according to a 2D top down vector,
        //concrete implementations of this pawn will add extra animation params in an override function
        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);

        //Debug.Log("updating animator with input: " + actualMovement);
    }

    #region Coroutines
    //Coroutine that smoothly changes the current movement vector towards the input movement vector
    IEnumerator AccelerationBlending()
    {
        isAccelBlending = true; //So outside scope knows if this coroutine is already running

        while (currMovement != inputMovement)
        {
            //unit vector pointing from _movement to _targetMovement
            Vector2 accelVec = (inputMovement - currMovement).normalized;
            //The new movement value, moves with a fixed speed
            Vector2 newMovement = currMovement + (accelVec * Time.deltaTime * (1 / accelTime));

            //If the distance from the new value is larger than the current value
            //This is in case it somehow overshoots the targetMovement
            if (Vector2.Distance(newMovement, inputMovement) > Vector2.Distance(currMovement, inputMovement))
            {
                currMovement = inputMovement;
            }
            else //This means the new movement is closer to the target movement
            {
                currMovement = newMovement;
            }

            yield return null;
        }

        isAccelBlending = false; //Tell outside scope that the coroutine is over
    }
    #endregion

    #region Helper Functions
    //Spherical interpolates this transform rotation towards a given lookdirection this frame
    private void slerpYRot(Vector2 lookDir)
    {
        float desiredYRot = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.y), Vector3.up).eulerAngles.y;

        Quaternion newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, desiredYRot, transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 8 * Time.deltaTime);
    }
    //Vector3 overload for above function
    private void slerpYRot(Vector3 lookDir)
    {
        slerpYRot(new Vector2(lookDir.x, lookDir.z));
    }
    #endregion
}
