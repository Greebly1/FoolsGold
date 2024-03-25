using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public List<enemySpawn> EnemySpawns;
}

[System.Serializable]
public class enemySpawn
{
    public GameObject enemyPrefab = null;
    public int spawnAmount = 1;
}