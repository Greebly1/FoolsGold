using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadEvents : MonoBehaviour
{
    public void TitleInput()
    {
        GameManager.TitleInputInvoke();
    }

    public void ChangeScene(string sceneName)
    {
        GameManager.instance.setScene(sceneName);
    }

    public void ChangeSceneEnum(level levelEnum)
    {
        GameManager.instance.setScene(levelEnum);
    }

    public void Quit()
    {
        GameManager.instance.Quit();
    }
}
