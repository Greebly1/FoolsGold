using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.UI;

//Controller designed for open world adventure
public class CamController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] protected GameObject camPivot;
    [SerializeField] float maxZoom = 40;
    [SerializeField] float minZoom = 8;
    [SerializeField] float maxXRot = 80;
    [SerializeField] float minXRot = 20;
    [SerializeField] float xRotationSmoothingTime = 0.2f;
    [SerializeField] float yRotationSmoothingTime = 0.2f;
    [SerializeField] float zoomSmoothingTime = 0.2f;


    float _inputZoom;
    public float inputZoom
    {
        get { return _inputZoom; }
        set { 
            _inputZoom = Mathf.Clamp(value, minZoom, maxZoom);
            if (!isZoomSmoothing)
            {
                zoomSmoothing = StartCoroutine("zoomLerp");
            }
        }
    }

    float _inputXRotation;
    public float inputXRotation
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

    protected Vector2 inputForward;

    Coroutine xRotationSmoothing;
    bool isXRotationSmoothing = false;

    Coroutine yRotationSmoothing;
    bool isYRotationSmoothing = false;

    Coroutine zoomSmoothing;
    bool isZoomSmoothing = false;


    public float currZoom
    {
        get { return Vector3.Distance(transform.position, camPivot.transform.position); }
    }
    public float currXRotation
    {
        get { return camPivot.transform.rotation.eulerAngles.x; }
    }
    public float currYRotation
    {
        get { return camPivot.transform.rotation.eulerAngles.y; }
    }

    public bool tryTargetObject(GameObject newTarget)
    {
        target = newTarget;
        return true;
    }

    private void Awake()
    {
        if (target == null)
        {
            Debug.LogWarning("No target inside of client camera controller");
        }
        inputXRotation = currXRotation;
        inputZoom = currZoom;
    }

    private void Update()
    {
        //if there is a target, constantly smoothdamp to it
    }

    void setZoom(float distance)
    {
        float amount = distance;
        if (distance >= maxZoom) { amount = maxZoom; }
        else if (distance <= minZoom) { amount = minZoom; }

        transform.position = camPivot.transform.position + (-transform.forward * amount);
    }

    void setRotation(float? x = null, float? y = null, float? z = null)
    {;
        float currZRotation = camPivot.transform.rotation.eulerAngles.z;

        camPivot.transform.rotation = Quaternion.Euler(x ?? currXRotation, y ?? currYRotation, z ?? currZRotation);
    }


    IEnumerator xRotationLerp()
    {
        Debug.Log("Starting xRotation Coroutine");
        isXRotationSmoothing = true;

        float velocity = 0;

        while (currXRotation != inputXRotation)
        {
            float newX = Mathf.SmoothDampAngle(currXRotation, inputXRotation, ref velocity, xRotationSmoothingTime);

            if (Mathf.Abs(currXRotation - inputXRotation) < 0.1f)
            {
                setRotation(x: inputXRotation);
                break;
            } else
            {
                setRotation(x: newX);
            }
            
            yield return null;
        }

        isXRotationSmoothing = false;
        Debug.Log("Ending xRotation Coroutine");
    }

    IEnumerator yRotationLerp()
    {
        Debug.Log("Starting yRotation Coroutine");
        isYRotationSmoothing = true;
        //TODO: Add y rotation
        yield return null;

        isYRotationSmoothing = false;
        Debug.Log("Ending yRotation Coroutine");
    }

    IEnumerator zoomLerp()
    {
        Debug.Log("Starting zoom coroutine");
        isZoomSmoothing = true;

        float velocity = 0;

        while (currZoom != inputZoom)
        {
            float newZoom = Mathf.SmoothDamp(currZoom, inputZoom, ref velocity, zoomSmoothingTime);
            
            if (Mathf.Abs(newZoom - inputZoom) < 0.1f)
            {
                setZoom(inputZoom);
                break;
            } else
            {
                setZoom(newZoom);
            }

            yield return null;
        }

        isZoomSmoothing = false;
        Debug.Log("Ending zoom coroutine");
    }
}

