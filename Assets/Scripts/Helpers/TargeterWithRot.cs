using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterWithRot : Targeter
{
    protected override void DoFollow()
    {
        base.DoFollow();
        transform.rotation = target.rotation;
    }
}
