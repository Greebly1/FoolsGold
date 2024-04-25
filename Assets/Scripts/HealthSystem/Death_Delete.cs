using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death_Delete : MonoBehaviour, IKillable
{
    [SerializeField] float destroyDelay = 0f;
    Coroutine routine = null;
    public UnityEvent DestroyInitiated;

    public void Kill()
    {
        routine = StartCoroutine("killCoroutine");
    }


    protected virtual IEnumerator killCoroutine()
    {
        float timeToLive = destroyDelay;

        while(timeToLive > 0f)
        {
            timeToLive -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        DestroyInitiated?.Invoke();
    }
}
