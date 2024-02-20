using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GlobalInputEvents
{
    public static Action<GameObject> SelectedObject; // For UI input to be interfaced with outside of it, decoupling the systems

}

public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput Pawn_Input;
    public PlayerInput Camera_Input;
    public PlayerInput UI_Input;

    public GameObjectEvent OnSelectObject;

    #region Monobehavior Callbacks
    void Awake()
    {
        GlobalInputEvents.SelectedObject += SelectedNewObject;
    }

    void SelectedNewObject(GameObject selectedObject)
    {
        OnSelectObject.Invoke(selectedObject);
    }

    #endregion
}


