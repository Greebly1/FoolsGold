using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This is a utility component that I use to signal to the game's systems that listen for it (ex: the enemy wave manager) that an enemy has died
public class EnemyDiedEvent : MonoBehaviour
{
    public static event Action EnemyDied;

    private void OnDestroy()
    {
        EnemyDied?.Invoke();
    }
}
