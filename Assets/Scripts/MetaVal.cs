using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// A MetaVal is just a variable that invokes a 'valueChanged' unity event when changed
/// </summary>
public class MetaVal<type> where type : IComparable
{
    public event Action valueChanged; //T0 - amount of change, T1 - new value
    private type _value;
    public type value
    {
        get { return _value; }
        private set
        {
            int deltaVal = _value.CompareTo(value);
            _value = value;
            if (deltaVal != 0)
            {
                valueChanged.Invoke();
            }
        }
    }

    public MetaVal(type initialValue)
    {
        _value = initialValue;
    }
}
