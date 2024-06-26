using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class Status : MonoBehaviour
{
    #region vars
    #region editor vars
    [SerializeField] float initialHealthPercent = 1;
    [SerializeField] int maxHealth = 20;
    [SerializeField]
    [DamageType]
    int baseResistances = 0; //damage type bitmask
    int resistances = 0;
    #endregion

    #region Events
    public StatEvent healthChanged;
    public UnityEvent deathEvent; //health is <= 0
    #endregion

    #region state vars

    Stat health;
    public int currHealth
    {
        get { return health.current; }
        private set { //Publish an event when the _currhealth variable changes
            if (value != currHealth)
            {
                health.current = value;
                CheckDeath();
                healthChanged?.Invoke(health);
            }
        }
    }

    bool hasDied = false;

    List<ResistancePowerup> powerups = new List<ResistancePowerup>();

    #endregion

    #region Shorthand Getters
    public bool alive { get { return currHealth >= 0; } }
    public float currHealthAsPercent { get { return currHealth / maxHealth; } }
    #endregion

    #endregion

    #region Monobehavior Callbacks

    protected virtual void Awake()
    {
        health = new Stat(maxHealth, maxHealth);
        currHealth = (int)(initialHealthPercent/100 * maxHealth);
    }
    private void Update()
    {
        TickPowerUps(Time.deltaTime);
        CalculateResistance();
    }
    #endregion

    //seperate overloaded damage function that takes into account which team the damage came from

    public void Damage(Damage damage) {
        

        Pawn owningpawn = GetComponent<Pawn>();
        //Debug.Log("Damage is being called, health team is: " + owningpawn.team.ToString());
        if (owningpawn == null) { return; }
        if (damage.teamSource != owningpawn.team)
        {
            if(DamageUtility.CompareResistanceToDamage(resistances, damage.damagetype))
            {
                Damage(damage.amount);
            }
        } 
    }

    public void Damage(int damage)
    {
        //Debug.Log("took damage");
        currHealth -= damage;
    }

    public void Heal(int amount)
    {
        currHealth += amount;
    }

    //Figures out what the resistance bitmask is with this Status' resistance powerup list
    void CalculateResistance()
    {
        resistances = baseResistances;
        foreach(ResistancePowerup currentPowerup in powerups)
        {
            resistances = DamageUtility.AddDamageType(resistances, currentPowerup.powerup);
        }
        Debug.Log(resistances);
    }

    public void AddPowerup(ResistancePowerup newPowerup)
    {
        powerups.Add(newPowerup);
        powerups[powerups.Count-1].powerupVisualizer = Instantiate(newPowerup.visualEffectPrefab, transform);
        CalculateResistance();
    }

    //Updates the timetolive for each powerup, destroys them if their time is up
    //returns true is a powerup was destroyed
    bool TickPowerUps(float deltaTime)
    {
        List<ResistancePowerup> markedForDeletion = new List<ResistancePowerup>();

        foreach (ResistancePowerup currentPowerup in powerups)
        {
            currentPowerup.duration -= deltaTime;
            if (currentPowerup.duration <= 0) { 
                markedForDeletion.Add(currentPowerup);
                Destroy(currentPowerup.powerupVisualizer); //clear the visual effect
            }
        }

        if (markedForDeletion.Count > 0)
        { //one or more powerups have ended
            powerups = powerups.Except(markedForDeletion).ToList<ResistancePowerup>(); //get rid of those powerups from the list
            CalculateResistance(); 
            return true;
        }

        return false;
    }

    #region helper functions
    void CheckDeath()
    {
        if (!alive && !hasDied)
        {
            Debug.Log("Dead");
            hasDied = true;
            deathEvent?.Invoke();
        }
    }
    #endregion
}


[System.Serializable]
public struct Stat
{
    public int max;
    public int current;
    public Stat(int Max, int Current)
    {
        this.max = Max;
        this.current = Current;
    }
}