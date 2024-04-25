using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawns a weapon pickup prefab periodically whenever there isnt one
/// </summary>
public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] bool spawnOnStartup;
    GameObject pickup = null;
    bool hasPickup
    {
        get { return pickup != null; }
    }
    float spawnCooldown;
    bool waitingForCooldown
    {
        get { return spawnCooldown > 0; }
    }

    void resetCooldown()
    {
        spawnCooldown = spawnDelay;
    }

    private void Awake()
    {
        if (spawnOnStartup) { SpawnPickup(); }
        spawnCooldown = spawnDelay;
    }
    private void Update()
    {
        if (hasPickup == false)
        {
            if (waitingForCooldown)
            {
                spawnCooldown -= Time.deltaTime;
            } else
            {
                SpawnPickup();
                resetCooldown();
            }
        }
    }

    void SpawnPickup()
    {
        pickup = Instantiate(pickupPrefab, this.transform.position, this.transform.rotation);
    }
}
