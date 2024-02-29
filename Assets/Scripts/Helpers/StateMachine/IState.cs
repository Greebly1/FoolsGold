using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Tick();

    public void OnBegin();

    public void OnEnd();
}
