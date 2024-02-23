using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Cursor3D : MonoBehaviour
{
    public static Cursor3D ClientPlayerCursor;

    #region Vars
    [SerializeField] public float sphereCastRadius = 0.4f;
    [SerializeField] LayerMask prioritySelectionLayerMask; //First layermask checked, prioritize checking for targets meant to be selectable
    [SerializeField] LayerMask selectionLayerMask;

    GameObject _hoveredObject;
    [HideInInspector] public GameObject hoveredObject
    {
        get { return _hoveredObject; }
        private set { _hoveredObject = value; }
    }
    GameObject _selectedObject;
    [HideInInspector]
    public GameObject selectedObject
    {
        get { return _selectedObject; }
        private set
        {
            _selectedObject = value;
            GlobalInputEvents.SelectedObject.Invoke(selectedObject);
        }
    }

    #endregion




    #region InputEvent Handlers
    public void OnSelect()
    {
        try
        {
            selectedObject = hoveredObject;
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning(ex.Message);
            Debug.Log("Tried to select object, but nothing is hovered, defaulting to not selecting anything");
            return; //Early out if failed
        }
    }

    #endregion

    #region Monobehavior Callbacks
    void Awake()
    {
        if (ClientPlayerCursor == null)
        {
            ClientPlayerCursor = this;
        }
        else
        {
            Debug.LogWarning("There are two playerCursor3D objects");
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit sphereHitInfo;
        RaycastHit rayHitInfo;

        if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out rayHitInfo, 100000, selectionLayerMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = rayHitInfo.point;

            hoveredObject = rayHitInfo.transform.gameObject;
        }

        if (Physics.SphereCast(mouseRay.origin, sphereCastRadius, mouseRay.direction, out sphereHitInfo, 100000, prioritySelectionLayerMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = sphereHitInfo.point;

            hoveredObject = sphereHitInfo.transform.gameObject;
        }
    }
    #endregion
}
