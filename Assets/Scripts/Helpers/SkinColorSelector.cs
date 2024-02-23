using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkinColorSelector : MonoBehaviour
{
    public UnityEvent<Color, SkinColors.colorNames> colorChangedEvent = new UnityEvent<Color, SkinColors.colorNames>();
    public SkinColors.colorNames colorName;
    public Color colorColor;


    public void colorChanged(int value)
    {
        SkinColors.colorNames color = (SkinColors.colorNames)Enum.ToObject(typeof(SkinColors.colorNames), value);

        if (colorName != color ) { colorName = color;
            colorColor = SkinColors.colorsByName[colorName];
            colorChangedEvent.Invoke(colorColor, colorName);
        }
    }

}
