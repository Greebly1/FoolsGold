using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Player : Status
{
    HudEventsNotifier UiBindings = new HudEventsNotifier();

    protected override void Awake()
    {
        base.Awake();
        healthChanged.AddListener(UiBindings.updateHealthbar);
    }
}
