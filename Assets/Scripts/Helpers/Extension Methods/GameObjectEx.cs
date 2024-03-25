using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Rotate (or unrotate) an input vector around the y axis of a gameobject
    /// </summary>
    public static Vector2 rotateInput(this GameObject pivot, Vector2 inputVec, bool undoRot = false)
    {
        float yRot = pivot.transform.rotation.eulerAngles.y; //get this object's y rotation

        if (!undoRot) { yRot *= -1; } //create an inverse rotation if we want to undo a rotation

        //make a quaternion representing the y rotation
        Quaternion pivotRot = Quaternion.Euler(0, yRot, 0);

        //Multiply a vector3 made using the input vector by the quaternion, effectively rotating it
        Vector3 rotatedInput = pivotRot * new Vector3(inputVec.x, 0, inputVec.y);

        //Return a vector 2 made from the rotated input vector3
        return new Vector2(rotatedInput.x, rotatedInput.z);
    }


    public static T GetOrAddComponent<T>(this GameObject self) where T : MonoBehaviour 
    {
        T typecast = self.GetComponent<T>();

        if (typecast == null)
        {
            self.AddComponent<T>();
            typecast = self.GetComponent<T>();
        }

        return typecast;
    }

    public static Vector3 position(this GameObject self)
    {
        return self.transform.position;
    }
}