using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public static PlayerController ClientPlayerController { get; private set; }

    PlayerInput input;
    [SerializeField] CamController topDownCamera;

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
                possessedPawn.focusedOnTarget = true;
            }
        }
    }
    float timeSinceAiming = 0;
    Coroutine aimTimer;

    Vector2 _moveInput;
    Vector2 moveInput //Pretty bad for this to be public
    {
        get { return _moveInput; }
        set
        {
            _moveInput = value;
            possessedPawn.run(topDownCamera.rotateInput(_moveInput));
        }
    }
    float zoomInput = 0;
    #endregion

    #region cameraEvents
    public static event Action<Vector2> camRotate;
    public static event Action<GameObject> camSetTarget;
    public static event Action<float> camZoom;
    public static event Action<Vector2> camQuickTurn;
    #endregion

    #region inputEvent Handlers
    //Methods starting with 'On' are called by the unity inputSystem using sendMessages() from the playerInput Component
    public void OnWalk(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.magnitude > 0) { possessedPawn.lastMovedDirection = topDownCamera.rotateInput(moveInput); }
        

        //Debug.Log(moveInput);
    }
    public void OnSprint(InputValue value)
    {
        holdingSprint = !holdingSprint;
        possessedPawn.toggleSprint(holdingSprint);
    }
    public void OnZoom(InputValue inputValue)
    {
        float amount = inputValue.Get<float>();
        zoomInput = amount * 2;
        camZoom.Invoke(amount * 3);
        Debug.Log("Zoom");
    }
    public void OnCrouch()
    {
        possessedPawn.toggleCrouch();
    }
    public void OnToggleCamRotation(InputValue inputValue)
    {
        Vector2 amount = inputValue.Get<Vector2>();
        camRotate.Invoke(amount);
    }
    public void OnAlignCamOrientation()
    {
        camQuickTurn?.Invoke(new Vector2(possessedPawn.transform.forward.x, possessedPawn.transform.forward.z));
    }
    public void OnQuickTurn()
    {
        camQuickTurn?.Invoke(- new Vector2(topDownCamera.transform.forward.x, topDownCamera.transform.forward.z));
    }
    public void OnAimDownSights()
    {
        Debug.Log("aim");
        holdingAim = !holdingAim;
    }
    #endregion

    #region Monobehavior Callbacks
    void Awake()
    {
        ClientPlayerController = this;
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.currentControlScheme == "M&K")
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane camFocusPlane = new Plane(Vector3.up, topDownCamera.target.transform.position);
            float distanceIntersection;

            if (camFocusPlane.Raycast(mouseRay, out distanceIntersection))
            {
                transform.position = mouseRay.GetPoint(distanceIntersection);
            }
        } else if (input.currentControlScheme == "Controller")
        {
            topDownCamera.inputZoom -= zoomInput;
        }

        moveInput = moveInput;
        possessedPawn.lookTarget = transform.position;
    }

    #endregion

    #region Coroutines
    IEnumerator LastAimedTimer()
    {
        while (!holdingAim)
        {
            timeSinceAiming += Time.deltaTime;
            if (timeSinceAiming > 0.3f) { possessedPawn.focusedOnTarget = false; }
            yield return null;
        }
        timeSinceAiming = 0;
    }

    #endregion
}
