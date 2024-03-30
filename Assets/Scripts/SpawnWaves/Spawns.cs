using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static List<Spawn> Spawns = new List<Spawn>();
    
    public static List<Spawn> SortByDistance()
    {
        Spawns = Spawns.OrderByDescending(item => item.DistanceFromPlayer).ToList();

        return Spawns;
    }

    private void Awake()
    {
        Spawns.Add(this);
    }

    public float DistanceFromPlayer
    {
        get
        {
            GameObject player = PlayerController.ClientPlayerController.possessedPawn.gameObject;
            if (player == null ) { return 0; }
            
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }

    public void SpawnObject(GameObject prefab)
    {
        Instantiate(prefab, this.transform.position, this.transform.rotation);
    }

    private void OnDestroy()
    {
        Spawns.Remove(this);
    }
}
