using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon actions serve as the event handlers of the weapon event publishers
/// These respond to the weapon events as if they were weapon attachments
/// Events must be connected inside editor
/// </summary>
[RequireComponent(typeof(Weapon))]
public class WeaponAction : GameAction //Monobehavior
{
    #region vars

    #region component references
    protected Weapon weapon;
    #endregion

    #endregion

    #region Monobehavior Callbacks
    protected override void Awake()
    {
        base.Awake();
        weapon = GetComponent<Weapon>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion


}
