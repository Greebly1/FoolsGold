using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public static PlayerController ClientPlayerController { get; private set; }

    #region input state variables
    bool holdingSprint = false;
    bool _holdingAim = false;
    bool holdingAim
    {
        get { return _holdingAim;}
        set { _holdingAim = value;
            if (!_holdingAim)
            {
                aimTimer = StartCoroutine("LastAimedTimer");
            } else
            {
                try { possessedPawn.lookTarget.setTarget(Cursor3D.ClientPlayerCursor.transform, follow: true); }
                catch (NullReferenceException ex)
                {
                    Debug.LogWarning(ex.Message);
                    Debug.LogWarning("Defaulting to player controller as targeting reference");
                    possessedPawn.lookTarget.setTarget(transform, follow: true);
                }
                possessedPawn.lookAtTarget = true;
            }
        }
    }
    float timeSinceAiming = 0;
    Coroutine aimTimer;

    Vector2 _moveInput;
    Vector2 moveInput 
    {
        get { return _moveInput; }
        set
        {
            _moveInput = value;

            try { possessedPawn.setMoveVec(moveInput.Rotated(Camera.main.transform.rotation.eulerAngles.y, true)); }
            catch (NullReferenceException ex)
            {
                Debug.LogWarning(ex.Message);
                Debug.LogError("there is no camera.main for the pawn input to be rotated locally, default to no rotation");
                possessedPawn.setMoveVec(moveInput);
            }
        }
    }
    #endregion

    #region inputEvent Handlers
    //Methods starting with 'On' are called by the unity inputSystem using sendMessages() from the playerInput Component
    public void OnWalk(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //if (moveInput.magnitude > 0) { possessedPawn.lastMovedDirection = topDownCamera.gameObject.rotateInput(moveInput, true); }
        

        //Debug.Log(moveInput);
    }
    public void OnSprint(InputValue value)
    {
        holdingSprint = !holdingSprint;

        //since the OnSprint event has been published it is safe to say that we are possessing a humanoid pawn
        (possessedPawn as HumanoidPawn).toggleSprint(holdingSprint);
    }
    public void OnCrouch()
    {
        //since the OnCrouch event has been published it is safe to say that we are possessing a humanoid pawn
        (possessedPawn as HumanoidPawn).toggleCrouch();
    }
    public void OnAimDownSights()
    {
        Debug.Log("aim");
        holdingAim = !holdingAim;
    }
    public void OnPrimaryFire()
    {
        (possessedPawn as HumanoidPawn).Attack();
    }
    #endregion

    #region Monobehavior Callbacks
    protected override void Awake()
    {
        base.Awake();
        if (ClientPlayerController != null) { 
            Debug.LogWarning("There are two player controllers!"); 
            Destroy(this.gameObject);
            return;
        }
        ClientPlayerController = this;
    }

    private void Update()
    {
        
        moveInput = moveInput;
    }

    #endregion


    #region Coroutines
    IEnumerator LastAimedTimer()
    {
        while (!holdingAim)
        {
            timeSinceAiming += Time.deltaTime;
            if (timeSinceAiming > 0.3f) { possessedPawn.lookAtTarget = false; }
            yield return null;
        }
        timeSinceAiming = 0;
    }

    #endregion

}
