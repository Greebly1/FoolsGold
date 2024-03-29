using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelFlow")]
public class LevelFlow : ScriptableObject
{
    [SerializeField] SerializedDictionary<level, string> _SceneDictionary = new SerializedDictionary<level, string>();
    public SerializedDictionary<level, string> SceneDictionary { 
        get { return _SceneDictionary; } 
    }
}


[System.Serializable]
public enum level
{
    Title,
    MainMenu,
    Level1,
    GameOver,
    PauseMenu
}