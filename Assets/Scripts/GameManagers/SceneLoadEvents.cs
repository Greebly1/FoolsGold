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

    public void Quit()
    {
        GameManager.instance.Quit();
    }
}
