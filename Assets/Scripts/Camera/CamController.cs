using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.UI;
using AYellowpaper.SerializedCollections;
using UnityEngine.InputSystem;

//Controller designed for open world adventure
public class CamController : MonoBehaviour
{
    #region Editor Vars
    [SerializeField] public GameObject target; //Target that the camera pivot follows
    [SerializeField] public Camera controlledCamera; //Parent transform of this camera
    [SerializeField] camTargetMode initMode = camTargetMode.followPlayer; //that starting mode of the camera

    //Encapsulates camera parameters for easy switching
    [SerializeField] CamParams currCamParams;
    [SerializeField] SerializedDictionary<camTargetMode, CamParams> ParamsByCamMode; //uses SerializableDictionary plugin
    #endregion

    camTargetMode _targetMode = camTargetMode.followPlayer;
    public camTargetMode targetMode
    {
        get { return _targetMode; }
        set { _targetMode = value;
            currCamParams = ParamsByCamMode[targetMode];
        }
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

    #region Shorthand Getters
    //The following getters/setters help make this script a bit more readible
    public Vector2 camFoward //Forward vector as a vector2
        { 
        get {
            Vector3 fwd = controlledCamera.transform.forward;
            return new Vector2(fwd.x, fwd.z);
        }
    } 
    float currZoom //The distance from the camera to the pivot
    {
        get { return Vector3.Distance(controlledCamera.transform.position, transform.position); }
        set { setZoom(value); }
    } 
    float currXRot //The x rotation of the pivot
    {
        get { return transform.rotation.eulerAngles.x; }
        set { setRotation(x:  value); }
    } 
    float currYRot //The y rotation of the pivot
    {
        get { return transform.rotation.eulerAngles.y; }
        set { setRotation(y: value); }
        
    }
    Vector3 currPosition //The position of the pivot
    {
        get { return transform.position; }
        set { transform.position = value; }
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
        targetMode = initMode;
        inputXRot = currXRot;
        inputYRot = currYRot;
        inputZoom = currZoom;

        if (target == null)
        {
            Debug.LogWarning("No target inside of client camera controller");
        }
        else
        {
            TryTargetObject(target);
        }

    }

    private void OnEnable()
    {
        targetMode = targetMode;
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
    }
    #endregion

    #region inputEventHandling
    //Functions that start with 'On' should be called by a playerInput component on a parent gameobject using unity broadcast message
    public void OnZoom(InputValue value)
    {
        float input = value.Get<Vector2>().y;

        inputZoom -= input;
    }

    public void OnAlignYRotation(InputValue value)
    {
        try
        {
            Vector3 playerForward = PlayerController.ClientPlayerController.possessedPawn.transform.forward;
            inputForward = new Vector2(playerForward.x, playerForward.z);
        } 
        catch (NullReferenceException ex)
        {
            Debug.LogWarning(ex.Message);
            Debug.LogWarning("Player pawn is null, using default value");
            inputForward = inputForward;
        }
    }

    public void OnQuickTurn(InputValue value)
    {
        inputForward = -inputForward;
    }

    public void OnCamRotation(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        inputXRot -= input.y;

        if (targetMode == camTargetMode.selectedObject) { inputYRot += input.x; }
    }

    public void SelectObject(GameObject newTarget)
    {
        ICamTargetable idealTarget = newTarget.GetComponent<ICamTargetable>();
        if (idealTarget != null) {
            if (target == idealTarget.CamTransform()) { return; } //Early Out
            target = idealTarget.CamTransform(); 
        } else 
        {
            return; //Early out
        }
        inputXRot = inputXRot;
        inputYRot = inputYRot;
        inputZoom = inputZoom;
    }
    #endregion

    #region Helper Functions
    void TryTargetObject(GameObject newTarget)
    {
        ICamTargetable idealTarget = newTarget.GetComponent<ICamTargetable>();
        target = idealTarget.CamTransform() ?? newTarget;
    }


    //Makes it easy to set the distance from the camPivot, effetively sets the zoom
    void setZoom(float distance)
    {
        float amount = distance;
        if (distance >= maxZoom) { amount = maxZoom; }
        else if (distance <= minZoom) { amount = minZoom; }

        controlledCamera.transform.position = transform.position + (-transform.forward * amount);
    }
    //Makes it easy to set just one (or more) value(s) of the rotation inside the campivot transform
    void setRotation(float? x = null, float? y = null, float? z = null)
    {
        float currZRotation = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(x ?? currXRot, y ?? currYRot, z ?? currZRotation);
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
        yield return null;

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

