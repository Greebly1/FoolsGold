using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The base class for the pawn component
/// The pawn will be the central hub for interfacing with a character gameobject. It contains input values and events
/// Controllers possess pawns and update these input values/events
/// The pawn then sends these values out to their components
/// Movement is tightly coupled to the concrete implementation of pawn, due to the use of root motion
/// </summary>
[RequireComponent(typeof(Animator))]
public class Pawn : MonoBehaviour, ICamTargetable
{
    #region Vars
    public Targeter lookTarget; //Gameobject that this pawn can use to look at a position
    [SerializeField] float turnSpeed = 720; //Degrees per second this pawn can rotate at
    [SerializeField] float accelTime = 1.5f; //How many seconds it takes to accel from 0 to max speed, Higher values will feel clunkier, shorter values will feel snappier
    [HideInInspector] public bool lookAtTarget = false;
    [SerializeField] public GameObject camTarget;
    [SerializeField] float interactRadius = 1;

    public Team team = Team.noTeam;

    #region Components
    protected Animator AnimationController { get; private set; }
    #endregion

    protected Vector2 inputMovement = Vector3.zero;  //Describes the goal movement of this pawn (this is the input the controller gives this pawn)
    protected Vector2 currMovement = Vector3.zero; //Describes how this pawn is moving (this is the input given to the AnimationController)

    float _maxSpeedPercent = 1f; //controls the max speed of this pawn used to clamp input going into the animation controller
    float maxSpeedPercent
    {
        get { return _maxSpeedPercent; }
        set { _maxSpeedPercent = Mathf.Clamp(value, 0, 1); }
    }
    
    protected bool _takingInput = false;
    [HideInInspector]
    public bool takingInput
    {
        get { 
            return _takingInput && isAnimatorValid; //only take input if the animator is also working
        }
        set { 
            _takingInput = value; 
        }
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

    protected bool isAnimatorValid
    {
        get
        {
            return AnimationController != null && AnimationController.isActiveAndEnabled;
        }
    }
    #endregion

    #endregion

    public UnityEvent OnDeath;


    #region Monobehavior Callbacks

    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        if (lookTarget == null) { Debug.LogError("A pawn does not have a targeter"); }
    }

    private void Update()
    {

        //If you are meant to look at the target, but the target is too close
        if (lookAtTarget && Vector3.Distance(new Vector3(lookTargetX, 0, lookTargetZ), new Vector3(transform.position.x, 0, transform.position.z)) < 0.4)
        {
            //do nothing
        } else if (lookAtTarget) //if you are meant to look at the target
        {
            //Look at the target
            Vector3 lookDirection = new Vector3(lookTargetX, 0, lookTargetZ) - new Vector3(transform.position.x, 0, transform.position.z);
            slerpYRot(lookDirection);
        } else if (currSpeed > 0) //if you are moving
        {
            //Look in the direction you are moving
            slerpYRot(currMovement);
        } else
        {
            //Default to doing nothing, we don't need to rotate
        }


        UpdateAnimator();
    }

    #endregion

    #region Input Functions
    public void setMoveVec(Vector2 input)
    {
        //early out
        if (!takingInput) { return; } //ragdolling or another mechanic has likely disabled input

        inputMovement = Vector2.ClampMagnitude(input, 1);
        //If the coroutine isn't running, start the coroutine
        if (!isAccelBlending) { accelBlending = StartCoroutine("AccelerationBlending"); }
    }

    public virtual void ResetInput()
    {
        inputMovement = new Vector2(0,0);
    }

    public virtual void ResetAnimator()
    {
        AnimationController.SetFloat("Right", 0);
        AnimationController.SetFloat("Forward", 0);
    }
    public virtual void Interact()
    {
        //early out
        if (!takingInput) { return; } //ragdolling or another mechanic has likely disabled input

        Collider[] interactTargets = Physics.OverlapSphere(transform.position, interactRadius);

        foreach (Collider target in interactTargets)
        {
            try
            {
                IInteractable damagable = target.GetComponent<IInteractable>();
                damagable.interact(this.gameObject);
            }
            catch { /*the thingamabob is not interactable */ }

        }
    }
    #endregion

    protected virtual void UpdateAnimator()
    {
        //early out
        if (!takingInput) { return; } //the animator is likely null or inactive due to ragdoll, or another mechanic has disabled input

        Vector2 actualMovement = Vector2.ClampMagnitude(inputMovement, maxSpeedPercent); 

        //The input must be rotated so it is no longer relative to the rotation of this pawn, instead it must be relative to the world
        actualMovement = this.gameObject.rotateInput(actualMovement);

        //All animation controllers can move according to a 2D top down vector,
        //concrete implementations of this pawn will add extra animation params in an override function
        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);

        //Debug.Log("updating animator with input: " + actualMovement);
    }

    #region Interfaces
    public GameObject CamTransform() { return camTarget ?? this.gameObject; }

    #endregion

    #region Coroutines
    //Coroutine that smoothly changes the current movement vector towards the input movement vector
    IEnumerator AccelerationBlending()
    {
        isAccelBlending = true; //So outside scope knows if this coroutine is already running

        while (currMovement != inputMovement && isAnimatorValid)
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
        //early out
        if (!takingInput) { return; } //dont let it rotate

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


    //These functions are called from other components that are expected to be on a pawn gameobject such as a health component via the sendmessages function
    #region Message Responders
    public void Dead()
    {
        OnDeath.Invoke();
    }
    #endregion
}
