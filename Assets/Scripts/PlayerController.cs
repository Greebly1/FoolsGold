using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public static PlayerController ClientPlayerController { get; private set; }

    PlayerInput input;
    [SerializeField] CameraController topDownCamera;

    //TODO
    //Recap how to use the new input system
    //create the camera follow with mouse look system

    public void OnWalk(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        possessedPawn.run(input);
    }

    public void OnSprint(InputValue value)
    {

        possessedPawn.toggleSprint();
    }

    public void OnZoom(InputValue inputValue)
    {
        float amount = inputValue.Get<float>();
        topDownCamera.Zoom(amount);
        Debug.Log(amount);
    }

    void Awake()
    {
        ClientPlayerController = this;
    }
}
