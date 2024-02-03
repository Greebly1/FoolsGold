using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.UI;

//Controller designed for open world adventure
public class CamController : MonoBehaviour
{
    #region Editor Vars
    [SerializeField] public GameObject target; //Target the campivot follows
    [SerializeField] public GameObject camPivot; //Parent transform of this camera

    //Encapsulates camera parameters for easy switching
    [SerializeField] CamParams currCamParams;
    #endregion

    camTargetMode _targetMode = camTargetMode.selectedObject;
    public camTargetMode targetMode
    {
        get { return _targetMode; }
        set { _targetMode = value; }
    }

    #region inputVars
    float _inputZoom;
    public float inputZoom
    {
        get { return _inputZoom; }
        set {
            float newZoom = Mathf.Clamp(value, minZoom, maxZoom);
            if (!isZoomSmoothing)
            {
                _inputZoom = newZoom;
                zoomSmoothing = StartCoroutine("zoomLerp");
            } else if (newZoom != _inputZoom)
            {
                _inputZoom = newZoom;
            }
        }
    }

    float _inputXRotation;
    public float inputXRot
    {
        get { return _inputXRotation; }
        set {
            _inputXRotation = Mathf.Clamp(value, minXRot, maxXRot);
            if (!isXRotationSmoothing)
            {
                xRotationSmoothing = StartCoroutine("xRotationLerp");
            }
        }
    }

    float _inputYRotation; //The setter starts a coroutine if it is a new value and there isnt already a coroutine
    float inputYRot
    {
        get { return _inputYRotation; }
        set { if (value != _inputYRotation) {
                _inputYRotation = value;
                if (!isYRotationSmoothing) { yRotationSmoothing = StartCoroutine("yRotationLerp"); }
            } 
        }
    }
    

    //This is a special setter for inputYRotation that uses direction instead of angle
    //Give it a Vector2 and it will set the nearest cardinal direction for inputYRotation in degrees
    public Vector2 inputForward
    {
        get
        {
            Vector3 fwd = Quaternion.Euler(0, inputYRot, 0) * Vector3.forward;
            return new Vector2(fwd.x, fwd.z);
        }
        set
        {
            float angleToForward = Vector2.SignedAngle(new Vector2(0, 1), value);
            float angleQuantize = -Mathf.Round(angleToForward / 45) * 45;

            inputYRot = angleQuantize;
        }
    }
    #endregion

    #region Coroutine Vars
    Coroutine xRotationSmoothing;
    bool isXRotationSmoothing = false;

    Coroutine yRotationSmoothing;
    bool isYRotationSmoothing = false;

    Coroutine zoomSmoothing;
    bool isZoomSmoothing = false;
    #endregion

    Vector3 velocity = Vector3.zero; //used for positional smoothing within Update(), Vector3.smoothdamp() requires a velocity variable passed as reference
    bool isObservingInputEvents = false;

    #region Shorthand Getters
    //The following getters/setters help make this script a bit more readible
    public Vector2 camFoward //Forward vector as a vector2
        { 
        get {
            Vector3 fwd = camPivot.transform.forward;
            return new Vector2(fwd.x, fwd.z);
        }
    } 
    float currZoom //The distance from the camera to the pivot
    {
        get { return Vector3.Distance(transform.position, camPivot.transform.position); }
        set { setZoom(value); }
    } 
    float currXRot //The x rotation of the pivot
    {
        get { return camPivot.transform.rotation.eulerAngles.x; }
        set { setRotation(x:  value); }
    } 
    float currYRot //The y rotation of the pivot
    {
        get { return camPivot.transform.rotation.eulerAngles.y; }
        set { setRotation(y: value); }
    }
    Vector3 currPosition //The position of the pivot
    {
        get { return camPivot.transform.position; }
        set { camPivot.transform.position = value; }
    }
    float maxZoom { get { return currCamParams.maxZoom; } }
    float minZoom { get { return currCamParams.minZoom; } }
    float maxXRot { get { return currCamParams.maxXRot; } }
    float minXRot { get { return currCamParams.minXRot; } }
    float xRotationSmoothingTime { get { return currCamParams.xRotationSmoothingTime; } }
    float yRotationSmoothingTime { get { return currCamParams.yRotationSmoothingTime; } }
    float zoomSmoothingTime { get { return currCamParams.zoomSmoothingTime; } }
    float positionSmoothTime { get { return currCamParams.positionSmoothTime; } }
    #endregion

    #region MonoBehavior Callbacks
    private void Awake()
    {
        if (target == null)
        {
            Debug.LogWarning("No target inside of client camera controller");
        } else
        {
            tryTargetObject(target);
        }
        inputXRot = currXRot;
        inputZoom = currZoom;
    }

    private void OnEnable()
    {
        observeCamEvents();
    }

    private void Update()
    {
        //if there is a target, constantly smoothdamp the campivot to it
        if (target != null)
        {
            currPosition = Vector3.SmoothDamp(currPosition, target.transform.position, ref velocity, positionSmoothTime);
        }
    }

    private void OnDisable()
    {
        ignoreCamEvents();
    }
    #endregion

    #region inputEventHandling
    //Responds to camRotate event from PlayerController
    void rotateEventHandle(Vector2 deltaRotation)
    {
        if (targetMode == camTargetMode.selectedObject)
        {
            inputYRot += deltaRotation.x;
        }

        inputXRot -= deltaRotation.y;
    }
    //Responds to camZoom event from PlayerController
    void zoomEventHandle(float deltaZoom)
    {
        inputZoom -= deltaZoom;
    }
    void tryTargetObject(GameObject newTarget)
    {
        CamTargetTransform idealTarget = newTarget.GetComponent<CamTargetTransform>();
        target = idealTarget.CamTransform ?? newTarget;
    }
    void quickTurnEventHandle(Vector2 newForward)
    {
        inputForward = newForward;
    }
    void observeCamEvents()
    {
        if (!isObservingInputEvents)
        {
            PlayerController.camRotate += rotateEventHandle;
            PlayerController.camZoom += zoomEventHandle;
            PlayerController.camSetTarget += tryTargetObject;
            PlayerController.camQuickTurn += quickTurnEventHandle;
            isObservingInputEvents = true;
        }
    }
    void ignoreCamEvents()
    {
        if (isObservingInputEvents)
        {
            PlayerController.camRotate -= rotateEventHandle;
            PlayerController.camZoom -= zoomEventHandle;
            PlayerController.camSetTarget -= tryTargetObject;
            PlayerController.camQuickTurn -= quickTurnEventHandle;
            isObservingInputEvents = false;
        }
    }
    #endregion

    #region Helper Functions
    //Makes it easy to set the distance from the camPivot, effetively sets the zoom
    void setZoom(float distance)
    {
        float amount = distance;
        if (distance >= maxZoom) { amount = maxZoom; }
        else if (distance <= minZoom) { amount = minZoom; }

        transform.position = camPivot.transform.position + (-transform.forward * amount);
    }
    //Makes it easy to set just one (or more) value(s) of the rotation inside the campivot transform
    void setRotation(float? x = null, float? y = null, float? z = null)
    {
        float currZRotation = camPivot.transform.rotation.eulerAngles.z;

        camPivot.transform.rotation = Quaternion.Euler(x ?? currXRot, y ?? currYRot, z ?? currZRotation);
    }

    //Rotates a vector2 by this camera's y axis rotation
    public Vector2 rotateInput(Vector2 inputVec)
    {
        //make a quaternion representing this cam's y rotation
        Quaternion camRotation = Quaternion.Euler(0, currYRot, 0);
        //Multiply a vector3 made using the input vector by the camrotation quaternion, rotating it
        Vector3 rotatedInput = camRotation * new Vector3(inputVec.x, 0, inputVec.y);
        //Return a vector 2 made from the rotated input vector3
        return new Vector2(rotatedInput.x, rotatedInput.z);
    }
    #endregion

    #region Coroutines
    IEnumerator xRotationLerp()
    {
        //Debug.Log("Starting xRotation Coroutine");
        isXRotationSmoothing = true;

        float velocity = 0;

        while (currXRot != inputXRot)
        {
            float newXRot = Mathf.SmoothDampAngle(currXRot, inputXRot, ref velocity, xRotationSmoothingTime);

            if (Mathf.Abs(currXRot - inputXRot) < 0.1f)
            {
                currXRot = inputXRot;
                break;
            } else
            {
                currXRot = newXRot;
            }
            
            yield return null;
        }

        isXRotationSmoothing = false;
        //Debug.Log("Ending xRotation Coroutine");
    }

    IEnumerator yRotationLerp()
    {
        //Debug.Log("Starting yRotation Coroutine");
        isYRotationSmoothing = true;
        float velocity = 0; //needed for smoothdamp

        while (currYRot != inputYRot)
        {
            float newYRot = Mathf.SmoothDampAngle(currYRot, inputYRot, ref velocity, yRotationSmoothingTime);

            if (Vector2.Angle(inputForward, camFoward) < 0.1)
            {
                currYRot = inputYRot;
                break;
            } else
            {
                currYRot = newYRot;
            }
            yield return null;
        }
        

        isYRotationSmoothing = false;
        Debug.Log("Ending yRotation Coroutine");
    }

    IEnumerator zoomLerp()
    {
        //Debug.Log("Starting zoom coroutine");
        isZoomSmoothing = true;

        float velocity = 0;

        while (currZoom != inputZoom)
        {
            float newZoom = Mathf.SmoothDamp(currZoom, inputZoom, ref velocity, zoomSmoothingTime);
            
            if (Mathf.Abs(newZoom - inputZoom) < 0.1f)
            {
                currZoom = inputZoom;
                break;
            } else
            {
                currZoom = newZoom;
            }

            yield return null;
        }

        isZoomSmoothing = false;
        //Debug.Log("Ending zoom coroutine");
    }
    #endregion
}

//Describes how the camPivot position behaves
public enum camTargetMode
{
    followPlayer,
    selectedObject,
    mouseRoaming
}

