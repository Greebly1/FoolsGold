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
    public UnityEvent deathEvent;
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

    #endregion

    #region Shorthand Getters
    public bool alive { get { return currHealth >= 0; } }
    public float currHealthAsPercent { get { return currHealth / maxHealth; } }
    #endregion

    #endregion

    #region Monobehavior Callbacks

    protected virtual void Awake()
    {
        currHealth = (int)(initialHealthPercent * maxHealth);
    }

    #endregion

    public void Damage(int amount)
    {
        Debug.Log("took damage");
        currHealth -= amount;
    }

    #region helper functions
    void CheckDeath()
    {
        if (!alive)
        {
            Debug.Log("Dead");
            deathEvent?.Invoke();
        }
    }
    #endregion
}
