using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// This singleton component should be on a canvas gameobject
/// </summary>
public class Hud : MonoBehaviour
{
    public static Hud Instance;

    [SerializeField] Image HealthBar;
    [SerializeField] TextMeshProUGUI HealthText;
    public void setHealthBar(Stat newHealth)
    {
        if (HealthBar != null && HealthText != null) {
            float healthPercent = Mathf.Clamp((float)newHealth.current, 0, (float)newHealth.max)/ (float)newHealth.max;
            Debug.Log(healthPercent);
            HealthText.text = (int)(healthPercent * 100) + "%";
            HealthBar.fillAmount = healthPercent;
        }
    }

    private void Awake()
    {
        //Enforcing singleton rule
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("TWO HUDS TRIED TO EXIST AT ONCE, DELETING ONE OF THEM");
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //we need to make sure to clear the singleton reference, because the gameobject this is attached to gets destroyed when the level unloads
        if (Instance == this) 
        {
            Instance = null;
        }
    }
}

//put this inside of wrapper classes that need to be able to interact with the Hud singleton
//Doing it this way means I have one area that I can easily switch out the HUD implementation for, like a jank form of dependency injection
//This also serves as a centralized area for singleton nullchecks
public class HudEventsNotifier 
{
    public void updateHealthbar(Stat newHealth)
    {
        Hud hudSingleton = Hud.Instance;
        if (hudSingleton != null )
        {
            hudSingleton.setHealthBar(newHealth);
        } else
        {
            Debug.LogWarning("HudEventsNotifier could not find a Hud");
        }
    }
}