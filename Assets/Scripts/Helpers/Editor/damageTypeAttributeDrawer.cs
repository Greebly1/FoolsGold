using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Property drawer for my bitmask damage types
/// </summary>
[CustomPropertyDrawer(typeof(DamageTypeAttribute))]
public class damageTypeAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// bools to contain state for each bit
    /// </summary>
    bool physical;
    bool ethereal;
    bool fire;
    bool shock;
    bool holy;
    bool psychic;

    readonly float vertScale = 16f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 7*vertScale + 2f; //hard code the property height to be the 
        //N * vertscale + 2f (2 for extra padding)
        //N is the number of property cells
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);

        //Set the bitmask bools to reflect what the current property value is
        setBools(property.intValue);

        //EditorGUI.PropertyField(position, property, label);
        //Rect labelRect = new Rect(position.x, position.y, position.width, 16f);
        //Rect buttonRect = new Rect(position.x, position.y = labelRect.height, position.width, 16f);
        EditorGUI.LabelField(offsetRect(position, 0), label, new GUIContent(property.intValue.ToString()));

        Rect checkboxRect = new Rect(position.x + 20f, position.y, position.width, position.height);
        physical = GUI.Toggle(offsetRect(checkboxRect, 1), physical, "Physical");
        ethereal = GUI.Toggle(offsetRect(checkboxRect, 2), ethereal, "Ethereal");
        fire = GUI.Toggle(offsetRect(checkboxRect, 3), fire, "Fire");
        shock = GUI.Toggle(offsetRect(checkboxRect, 4), shock, "Shock");
        holy = GUI.Toggle(offsetRect(checkboxRect, 5), holy, "Holy");
        psychic = GUI.Toggle(offsetRect(checkboxRect, 6), psychic, "Psychic");

        property.intValue = getBitmask;
        EditorGUI.EndProperty();
    }

    Rect offsetRect(Rect originalPos, int indexOffset = 0)
    {
        float heightOffset = indexOffset * vertScale;

        return new Rect(originalPos.x, originalPos.y + heightOffset, originalPos.width, 16f);
    }

    //Calculates the bitmask from the gui boolean flags
    int getBitmask
    {
        get {
            int output = 0; //all bits are off

            //if the gui boolean for a damage type is true, flip its bitflag on
            if (physical) output += (int)DamageType.physical;
            if (ethereal) output += (int)DamageType.ethereal;
            if (fire) output += (int)DamageType.fire;
            if (shock) output += (int)DamageType.shock;
            if (holy) output += (int)DamageType.holy;
            if (psychic) output += (int)DamageType.psychic;

            return output;
        }
    }
    

    void setBools(int bitmask)
    {
        //Set all the bitmask bools to false
        physical = false;
        ethereal = false;
        fire = false;
        shock = false;
        holy = false;
        psychic = false;

        //for each damage type, if the bitmask bit is on, flip the corresponding bool to true
        if (DamageUtility.ContainsType(bitmask, DamageType.physical)) physical = true;
        if (DamageUtility.ContainsType(bitmask, DamageType.ethereal)) ethereal = true;
        if (DamageUtility.ContainsType(bitmask, DamageType.fire)) fire = true;
        if (DamageUtility.ContainsType(bitmask, DamageType.shock)) shock = true;
        if (DamageUtility.ContainsType(bitmask, DamageType.holy)) holy = true;
        if (DamageUtility.ContainsType(bitmask, DamageType.psychic)) psychic = true;
    }
}

/*
 
    none = 0,
    physical = 1 << 0,
    ethereal = 1 << 1,
    fire = 1 << 2,
    shock = 1 << 3,
    holy = 1 << 4,
    psychic = 1 << 5,

 */