using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// The camera's position is constantly smoothdamping towards a position,
/// This position is definted by; this camera's forward vector, the position of the cameraTarget, and ths camZoom
/// The position is calculated with CalculateCamPosition
/// </summary>
public class CameraController : MonoBehaviour
{
    public GameObject selectedTarget;
    public GameObject cameraTarget;
    public CameraTargetMode camTargetMode = CameraTargetMode.Player;
    public CameraMode camMode = CameraMode.Combat;

    //shortcut to cameraTarget.transform.position
    public Vector3 targetPos {
        get { return cameraTarget.transform.position; }
        private set { cameraTarget.transform.position = value; } }
    public float camZoom = 15f; //Sort of represents the distance the camera will be from the cameraTarget
    public float minZoom = 5f;
    public float maxZoom = 20f;

    //This will give a unit vector that tells the direction the camera is facing with a y of 0
    public Vector3 camOrientation
    {
        get { return transform.forward.With(y: 0).normalized; }
    }

    public float initialXRotation = 60;
    public float localXRotation = 2;
    public float maxXRot = 70;
    public float minXRot = 30;

    void Awake()
    {
        tryTargetObject(selectedTarget);
    }

    void Update()
    {
        targetPos = selectedTarget.transform.position;

        transform.position = CalculateCameraPosition();
        transform.rotation = Quaternion.LookRotation(targetPos-transform.position, Vector3.up);
    }

    public void Zoom(float amount)
    {
        camZoom -= amount;
        camZoom = Mathf.Clamp(camZoom, maxZoom, minZoom);
    }

    public void RotateX(float amount)
    {
        localXRotation += amount;
        localXRotation = Mathf.Clamp(localXRotation, minXRot, maxXRot);
    }

    //Instead of using several functions to control camera transform in parallel,
    //This one function takes all virtual parameters and converts them into a single position
    //Abstracting position calculation this way consolidates all of my problems into one area and prevents dependent states from becoming misaligned. hopefully
    Vector3 CalculateCameraPosition()
    {
        //First -- According to the camZoom level, calculate the position of the camera if it was facing the origin with 0 rotation (as if it was on the XZ plane)
        Vector3 unrotatedCamPosOrigin = -camOrientation * camZoom;

        //Construct our rotator with the camera's local x axis
        Quaternion targetRotator = Quaternion.AngleAxis(localXRotation, transform.right);

        //Second -- Now we rotate around the origin by the rotator
        Vector3 rotatedCamPosOrigin = targetRotator * unrotatedCamPosOrigin;

        //Then we add the position vector of our target to slide over to it 
        Vector3 rotatedCamPosTarget = rotatedCamPosOrigin + targetPos;
        
        return rotatedCamPosTarget;
    }

    //Sets the objecttarget to the given object, it tries to find a valid camTargetTransform component, but if it does not it will just default to the object
    public bool tryTargetObject(GameObject target)
    {
        CamTargetTransform camTargetObject = target.GetComponent<CamTargetTransform>();
        if (camTargetObject != null)
        {
            selectedTarget = camTargetObject.CamTransform;
            return true;
        }

        selectedTarget = target;
        return false;
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

//useful vector extension methods found through Git-Amend
public static class Vector3Extensions
{
    //from a vector, set one or more of its values (x,y, and or z) to a given value(s)
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}