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
    bool camxrotationEnabled = false;
    bool holdingSprint = false;
    Vector2 _moveInput = Vector2.zero;
    public Vector2 moveInput //Pretty bad for this to be public
    {
        get { return _moveInput; }
        set
        {
            _moveInput = value;
            possessedPawn.run(rotateInput(_moveInput));
        }
    }
    float zoomInput = 0;

    //Methods starting with 'On' are called by the unity inputSystem
    //TODO:
    //when the player rotates while holding a walk it does not rotate the input to match until this input event runs again
    public void OnWalk(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //Debug.Log(input);
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
    }

    public void OnCrouch()
    {
        possessedPawn.toggleCrouch();
    }

    public void OnToggleCamRotation()
    {
        camxrotationEnabled = !camxrotationEnabled;
    }

    public void OnAimUpdate(InputValue value)
    {
        Vector2 amount = value.Get<Vector2>();

        if (camxrotationEnabled)
        {
            topDownCamera.inputXRotation -= amount.y;
        }

        
    }

    void Awake()
    {
        ClientPlayerController = this;
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        topDownCamera.inputZoom -= zoomInput;
        
          
        if (input.currentControlScheme == "M&K")
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane camFocusPlane = new Plane(Vector3.up, topDownCamera.target.transform.position);
            float distanceIntersection;

            if (camFocusPlane.Raycast(mouseRay, out distanceIntersection))
            {
                transform.position = mouseRay.GetPoint(distanceIntersection);
            }
        }

        possessedPawn.lookTarget = transform.position;
        //Probably not the best solution
        moveInput = moveInput; //Doing this updates the input inside the pawn in case it ends up rotating
    }

    //Rotates a inputVector according to the camera and the pawn, so the new input is relative to the camera
    Vector2 rotateInput(Vector2 inputVec)
    {
        Quaternion pawnRotation = Quaternion.Euler(0, possessedPawn.transform.rotation.eulerAngles.y, 0);
        Quaternion camRotation = Quaternion.Euler(0, topDownCamera.camPivot.transform.rotation.eulerAngles.y, 0);

        Vector3 rotatedInput = Quaternion.Inverse(pawnRotation) * new Vector3(inputVec.x, 0, inputVec.y);

        rotatedInput = camRotation * rotatedInput;

        return new Vector2(rotatedInput.x, rotatedInput.z);
    }
}
