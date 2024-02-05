using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColors
{
    public static Dictionary<colorNames, Color> colorsByName = new Dictionary<colorNames, Color>()
    {
        { colorNames.Ivory, new Color(233f / 255f, 203f / 255f, 167f / 255f, 1f) },
        { colorNames.Porcelain,  new Color(238f / 255f, 208f / 255f, 184f / 255f, 1f) },
        { colorNames.Pale_Ivory, new Color(247f / 255f, 221f / 255f, 196f / 255f, 1f) },
        { colorNames.Warm_Ivory,  new Color(247f / 255f, 226f / 255f, 171f / 255f, 1f) },
        { colorNames.Sand,  new Color(239f / 255f, 199f / 255f, 148f / 255f, 1f) },
        { colorNames.Rose_Beige, new Color(239f / 255f, 192f / 255f, 136f / 255f, 1f) },
        { colorNames.Limestone, new Color(231f / 255f, 188f / 255f, 145f / 255f, 1f) },
        { colorNames.Beige, new Color(236f / 255f, 192f / 255f, 131f / 255f, 1f) },
        { colorNames.Sienna, new Color(208f / 255f, 158f / 255f, 125f / 255f, 1f) },
        { colorNames.Honey, new Color(203f / 255f, 150f / 255f, 98f / 255f, 1f) },
        { colorNames.Band, new Color(171f / 255f, 139f / 255f, 100f / 255f, 1f) },
        { colorNames.Almond, new Color(148f / 255f, 98f / 255f, 61f / 255f, 1f) },
        { colorNames.Chestnut, new Color(136f / 255f, 86f / 255f, 51f / 255f, 1f) },
        { colorNames.Bronze, new Color(118f / 255f, 68f / 255f, 31f / 255f, 1f) },
        { colorNames.Umber, new Color(178f / 255f, 105f / 255f, 73f / 255f, 1f) },
        { colorNames.Golden, new Color(128f / 255f, 73f / 255f, 42f / 255f, 1f) },
        { colorNames.Espresso, new Color(98f / 255f, 58f / 255f, 23f / 255f, 1f) },
        { colorNames.Coffee, new Color(48f / 255f, 30f / 255f, 16f / 255f, 1f) }
    };

    /*
    public static Color Ivory = new Color(233 / 255, 203 / 255, 167 / 255, 1);
    public static Color Porcelain = new Color(238 / 255, 208 / 255, 184 / 255, 1);
    public static Color Pale_Ivory = new Color(247 / 255, 221 / 255, 196 / 255, 1);
    public static Color Warm_Ivory = new Color(247 / 255, 226 / 255, 171 / 255, 1);
    public static Color Sand = new Color(239 / 255, 199 / 255, 148 / 255, 1);
    public static Color Rose_Beige = new Color(239 / 255, 192 / 255, 136 / 255, 1);
    public static Color Limestone = new Color(231 / 255, 188 / 255, 145 / 255, 1);
    public static Color Beige = new Color(236 / 255, 192 / 255, 131 / 255, 1);
    public static Color Sienna = new Color(208 / 255, 158 / 255, 125 / 255, 1);
    public static Color Honey = new Color(203 / 255, 150 / 255, 98 / 255, 1);
    public static Color Band = new Color(171 / 255, 139 / 255, 100 / 255, 1);
    public static Color Almond = new Color(148 / 255, 98 / 255, 61 / 255, 1);
    public static Color Chestnut = new Color(136 / 255, 86 / 255, 51 / 255, 1);
    public static Color Bronze = new Color(118 / 255, 68 / 255, 31 / 255, 1);
    public static Color Umber = new Color(178 / 255, 105 / 255, 73 / 255, 1);
    public static Color Golden = new Color(128 / 255, 73 / 255, 42 / 255, 1);
    public static Color Espresso = new Color(98 / 255, 58 / 255, 23 / 255, 1);
    public static Color Dark_Chocolate = new Color(48 / 255, 30 / 255, 16 / 255, 1);
    */

    public enum colorNames
    {
        Ivory, Porcelain, Pale_Ivory, Warm_Ivory, Sand,
        Rose_Beige, Limestone, Beige, Sienna, Honey,
        Band, Almond, Chestnut, Bronze, Umber,
        Golden, Espresso, Coffee
    }
}


