using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Delete : MonoBehaviour, IKillable
{
    public void Kill()
    {
        Destroy(gameObject);
    }
}
