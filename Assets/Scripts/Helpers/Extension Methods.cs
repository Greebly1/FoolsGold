using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//useful vector extension methods found through Git-Amend
public static class Vector3Extensions
{
    //from a vector, set one or more of its values (x,y, and or z) to a given value(s)
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}

public static class Vector2Extensions
{
    //rotate a vector2 by degrees
    public static Vector2 Rotated(this Vector2 vector, float degrees, bool undoRotation = false)
    {
        float yRot = degrees;

        if (!undoRotation) { yRot *= -1; } //create an inverse rotation if we want to undo a rotation

        //make a quaternion representing the y rotation
        Quaternion pivotRot = Quaternion.Euler(0, yRot, 0);

        //Multiply a vector3 made using the input vector by the quaternion, effectively rotating it
        Vector3 rotatedInput = pivotRot * new Vector3(vector.x, 0, vector.y);

        //Return a vector 2 made from the rotated input vector3
        return new Vector2(rotatedInput.x, rotatedInput.z);
    }
}

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
}