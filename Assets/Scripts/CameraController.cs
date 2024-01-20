using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTarget;
    public CameraTargetMode camTargetMode = CameraTargetMode.Player;
    public CameraMode camMode = CameraMode.Combat;

    public Vector3 targetPos {
        get { return cameraTarget.transform.position; }
        private set { cameraTarget.transform.position = value; } }
    public float camZoom = 15f;

    public Vector3 camOrientation
    {
        get { return new Vector3(transform.forward.x, 0, transform.forward.z).normalized; }
    }

    void Awake()
    {

    }

    void Update()
    {
        targetPos = PlayerController.ClientPlayerController.possessedPawn.transform.position;

        transform.position = targetPos + (-transform.forward * camZoom) + camOrientation * (1.5f);
    }
}

public enum CameraTargetMode
{
    Player, Mouse, Object
}

public enum CameraMode
{
    Cinematic, Combat, Roaming
}