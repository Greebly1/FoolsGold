using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] LootTable lootPool;
    [SerializeField] Transform SpawnLocation;

    List<ItemDrop> randomLootList = new List<ItemDrop>(); 

    private void Awake()
    {
        foreach (ItemDrop item in lootPool.ItemsPossible)
        {
            for (int duplicatesAdded = 0; duplicatesAdded < item.dropWeight; duplicatesAdded++)
            {
                randomLootList.Add(item);
            }
        }
    }

    public void DropRandomItem()
    {
        int randomIndex = Random.Range(0, randomLootList.Count);
        if (randomLootList[randomIndex].item == null) { return; } // early out, dont drop an item we got unlucky, null item drop items represent a chance to not get anything

        Instantiate(randomLootList[randomIndex].item, SpawnLocation.position, SpawnLocation.rotation);
    }
}

[System.Serializable]
public class ItemDrop //a prefab and a dropchance
{
    public GameObject item;
    public int dropWeight;
}