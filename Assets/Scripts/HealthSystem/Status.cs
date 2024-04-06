using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Status : MonoBehaviour
{
    #region vars
    #region editor vars
    [SerializeField] float initialHealthPercent = 1;
    [SerializeField] int maxHealth = 20;
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

    #endregion

    //seperate overloaded damage function that takes into account which team the damage came from

    public void Damage(Damage damage) {
        

        Pawn owningpawn = GetComponent<Pawn>();
        //Debug.Log("Damage is being called, health team is: " + owningpawn.team.ToString());
        if (owningpawn == null) { return; }
        if (damage.teamSource != owningpawn.team)
        {
            Damage(damage.amount);
        } 
    }

    public void Damage(int damage)
    {
        //Debug.Log("took damage");
        currHealth -= damage;
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