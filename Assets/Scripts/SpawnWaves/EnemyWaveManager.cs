using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using Zenject.Asteroids;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Singleton;
    public bool randomWaves = false; //toggle to true for endless mode

    public List<Wave> allWaves;
    public int currWave = 0;
    public int enemiesAlive;

    public float waveDelayLength = 5f;
    public float waveDelayTimer = 5f;
    Coroutine spawnWaveCoroutine;

    private void Awake()
    {
        if (Singleton != null) { 
            Destroy(this);
            return;
        }
        Singleton = this;
        waveDelayTimer = waveDelayLength;

        
    }

    private void Start()
    {
        Debug.Log("Starting wave");
        SpawnNextWave();

        EnemyDiedEvent.EnemyDied += EnemyDied;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 )
        {
            SpawnNextWave();
        }
    }

    public void SpawnNextWave()
    {
        if(randomWaves)
        {
            SpawnRandomWave();
            return; //early out
        }
        if (currWave+1 >= allWaves.Count) { return; }
        spawnWaveCoroutine = StartCoroutine("SpawnWave", allWaves[currWave]);
        currWave++;
    }

    public void SpawnRandomWave()
    {
        currWave++;
        int randWaveIndex = Random.Range(0, allWaves.Count);
        spawnWaveCoroutine = StartCoroutine("SpawnWave", allWaves[randWaveIndex]);
    }

    IEnumerator SpawnWave(Wave waveToSpawn)
    {
        //Debug.Log("Starting wave delay");
        Spawn.SortByDistance();
        yield return new WaitForSeconds(waveDelayLength);
        //Debug.Log("exiting wave delay");

        int enemiesSpawnedThisWave = 0;

        //turn the list of spawns into a queue
        Queue<enemySpawn> waveAsQueue = new Queue<enemySpawn>(waveToSpawn.EnemySpawns);
        
        while (waveAsQueue.Count > 0) //while that list is not empty
        {
            enemySpawn currentEnemySpawn = waveAsQueue.Dequeue(); //take the first item out of that list

            int numberSpawned = 0; //keep track of how many times we have spawned the item

            while (currentEnemySpawn.spawnAmount > numberSpawned) //until the number we have spawned has reached the desired spawn amount
            {
                //spawn the item
                Spawn.Spawns[enemiesSpawnedThisWave].SpawnObject(currentEnemySpawn.enemyPrefab);
                numberSpawned++;
                enemiesSpawnedThisWave++;
                yield return null;
            }
        }
        enemiesAlive = enemiesSpawnedThisWave;
    }
}
