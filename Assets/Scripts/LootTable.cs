using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField] public List<ItemDrop> ItemsPossible;
}