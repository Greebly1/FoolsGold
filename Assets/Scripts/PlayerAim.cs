using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public static PlayerAim clientPlayerAim;
    [HideInInspector] public Vector2 aimInput = Vector2.zero; //This should be updated by the clientplayercontroller each frame


    private void Awake()
    {
        if (clientPlayerAim == null)
        {
            clientPlayerAim = this;
        } else if (clientPlayerAim != this)
        {
            Destroy(this.gameObject);
        }
    }


}
