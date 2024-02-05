using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorPreview : MonoBehaviour
{
    public Image img;

    public void colorChangeHandler(Color newcol, SkinColors.colorNames name) {
        img.color = newcol;
    }
}
