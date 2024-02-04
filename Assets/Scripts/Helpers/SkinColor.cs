using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColors
{
    public static Dictionary<colorNames, Color> colorsByName = new Dictionary<colorNames, Color>()
    {
        { colorNames.Ivory, new Color(233 / 255, 203 / 255, 167 / 255, 1) },
        { colorNames.Porcelain,  new Color(238 / 255, 208 / 255, 184 / 255, 1) },
        { colorNames.Pale_Ivory, new Color(247 / 255, 221 / 255, 196 / 255, 1) },
        { colorNames.Warm_Ivory,  new Color(247 / 255, 226 / 255, 171 / 255, 1) },
        { colorNames.Sand,  new Color(239 / 255, 199 / 255, 148 / 255, 1) },
        { colorNames.Rose_Beige, new Color(239 / 255, 192 / 255, 136 / 255, 1) },
        { colorNames.Limestone, new Color(231 / 255, 188 / 255, 145 / 255, 1) },
        { colorNames.Beige, new Color(236 / 255, 192 / 255, 131 / 255, 1) },
        { colorNames.Sienna, new Color(208 / 255, 158 / 255, 125 / 255, 1) },
        { colorNames.Honey, new Color(203 / 255, 150 / 255, 98 / 255, 1) },
        { colorNames.Band, new Color(171 / 255, 139 / 255, 100 / 255, 1) },
        { colorNames.Almond, new Color(148 / 255, 98 / 255, 61 / 255, 1) },
        { colorNames.Chestnut, new Color(136 / 255, 86 / 255, 51 / 255, 1) },
        { colorNames.Bronze, new Color(118 / 255, 68 / 255, 31 / 255, 1) },
        { colorNames.Umber, new Color(178 / 255, 105 / 255, 73 / 255, 1) },
        { colorNames.Golden, new Color(128 / 255, 73 / 255, 42 / 255, 1) },
        { colorNames.Espresso, new Color(98 / 255, 58 / 255, 23 / 255, 1) },
        { colorNames.Dark_Coffee, new Color(48 / 255, 30 / 255, 16 / 255, 1) }
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
        Golden, Espresso, Dark_Coffee
    }
}


