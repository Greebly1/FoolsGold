using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/CamParams")]
public class CamParams : ScriptableObject
{
    [SerializeField] public float maxZoom = 40;
    [SerializeField] public float minZoom = 8; //This may be changed when the cam switches modes
    [SerializeField] public float maxXRot = 80;
    [SerializeField] public float minXRot = 20; //This may also be changed when the cam switches modes

    [SerializeField] public float xRotationSmoothingTime = 0.2f;
    [SerializeField] public float yRotationSmoothingTime = 0.2f;
    [SerializeField] public float zoomSmoothingTime = 0.2f;
    [SerializeField] public float positionSmoothTime = 0.4f;
}
