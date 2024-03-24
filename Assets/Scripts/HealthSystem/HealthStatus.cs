using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthStatus : MonoBehaviour
{
    #region vars
    #region editor vars
    [SerializeField] float initialHealthPercent = 1;
    [SerializeField] int maxHealth = 20;
    #endregion

    #region Events
    public UnityEvent<float> healthChanged;
    public UnityEvent deathEvent; //health is <= 0
    #endregion

    #region state vars
    int _currHealth;
    public int currHealth
    {
        get { return _currHealth; }
        private set { //Publish an event when the _currhealth variable changes
            if (value != currHealth)
            {
                _currHealth = value;
                CheckDeath();
                healthChanged?.Invoke(currHealth);
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
        currHealth = (int)(initialHealthPercent/100 * maxHealth);
    }

    #endregion

    //seperate overloaded damage function that takes into account which team the damage came from

    public void Damage(Damage damage) {
        

        Pawn owningpawn = GetComponent<Pawn>();
        Debug.Log("Damage is being called, health team is: " + owningpawn.team.ToString());
        if (owningpawn == null) { return; }
        if (damage.teamSource != owningpawn.team)
        {
            Damage(damage.amount);
        } 
    }

    public void Damage(int damage)
    {
        Debug.Log("took damage");
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
