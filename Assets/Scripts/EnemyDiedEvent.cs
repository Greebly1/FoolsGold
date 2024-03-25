using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDiedEvent : MonoBehaviour
{
    public static event Action EnemyDied;

    private void OnDestroy()
    {
        EnemyDied?.Invoke();
    }
}
