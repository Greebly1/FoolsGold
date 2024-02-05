using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class characterCreator : MonoBehaviour
{
    public static string directory = "/SaveData";
    public static string fileName = "CharData.txt";

    public characterCreationParams settings = new characterCreationParams();

    public void saveCharacterParams(characterCreationParams characterSettings)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string JSONCharSettings = JsonUtility.ToJson(characterSettings);

        File.WriteAllText(dir + fileName, JSONCharSettings);
    }

    public void saveEventHandler()
    {
        saveCharacterParams(settings);
    }

    public void loadEventHandler()
    {
        loadCharParams(out settings);
    }

    public static bool loadCharParams(out characterCreationParams charSettings)
    {
        string filePath = Application.persistentDataPath + directory + fileName;
        bool wasSuccessful = false;
        characterCreationParams loadedSettings = new characterCreationParams();

        if (File.Exists(filePath))
        {
            string loadedJSON = File.ReadAllText(filePath);

            loadedSettings = JsonUtility.FromJson<characterCreationParams>(loadedJSON);

            wasSuccessful = true;
        } else
        {
            Debug.LogWarning("no save file exists");
        }

        charSettings = loadedSettings;
        return wasSuccessful;
    }

    
    #region setting Change Handlers
    //Responds to onValueChanged events from UI sliders
    public void genderChanged(Single value) => settings.gender = value;
    public void shouldersChanged(Single value) => settings.shoulderWidth = value;
    public void heightChanged(Single value) => settings.height = value;
    public void chestChanged(Single value) => settings.chestSize = value;
    public void waistChanged(Single value) => settings.waistSize = value;
    public void midesectionChanged(Single value) => settings.midsection = value;
    public void muscleChanged(Single value) => settings.muscle = value;
    public void fatChanged(Single value) => settings.fat = value;
    public void skinColorChanged(Color color, SkinColors.colorNames value2) => settings.skinColor = value2;
    #endregion
}

[System.Serializable]
public struct characterCreationParams
{
    public float gender;
    public float shoulderWidth;
    public float height;
    public float chestSize;
    public float waistSize;
    public float midsection;
    //build is normalized 
    public float muscle;
    public float fat;
    public SkinColors.colorNames skinColor;

    public characterCreationParams(float Muscle = 0.5f, float Fat = 0.5f, float Gender = 0, float ShoulderWidth= 1, float Height = 1, float ChestSize = 0.5f, float WaistSize = 0.5f, float MidSecion = 0.5f, SkinColors.colorNames SkinColor = SkinColors.colorNames.Beige)
    {
        gender = Gender;
        shoulderWidth = ShoulderWidth; 
        height = Height;
        chestSize = ChestSize;
        waistSize = WaistSize;
        midsection = MidSecion;
        muscle = Muscle;
        fat = Fat;
        skinColor = SkinColor;
    }
}


