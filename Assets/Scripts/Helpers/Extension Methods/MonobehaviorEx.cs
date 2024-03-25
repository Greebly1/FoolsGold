using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonobehaviorExtensions
{

    public static Vector3 position(this MonoBehaviour self)
    {
        return self.transform.position;
    }
}
