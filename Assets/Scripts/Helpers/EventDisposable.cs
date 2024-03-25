using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDisposable : MonoBehaviour
{
    public void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
