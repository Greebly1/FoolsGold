using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public static PlayerController ClientPlayerController { get; private set; }

    PlayerInput input;
    [SerializeField] CamController topDownCamera;
    bool camxrotationEnabled = false;
    bool holdingSprint = false;


    //Methods starting with 'On' are called by the unity inputSystem
    public void OnWalk(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        possessedPawn.run(input);
    }

    public void OnSprint(InputValue value)
    {
        holdingSprint = !holdingSprint;
        possessedPawn.toggleSprint(holdingSprint);
    }

    public void OnZoom(InputValue inputValue)
    {
        float amount = inputValue.Get<float>();
        topDownCamera.inputZoom -= amount * 4;
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
    }
}
