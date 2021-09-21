using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MySystem
{
    public static string setCharAt(this string s, int index, char c)
    {
        return s.Substring(0, index) + c + s.Substring(index + 1);
    }

    public static char getCharAt(this string s, int index)
    {
        return s.Substring(index, 1).ToCharArray()[0];
    }

    public static void defaultCycleMetaDataAt(this TileData td, int index)
    {
        if (td.metadata.getCharAt(index) == '0') td.metadata = td.metadata.setCharAt(index, '1');
        else td.metadata = td.metadata.setCharAt(index, '0');
    }

    public static Transform getChildWithName(this Transform t, string name)
    {

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).name == name)
            {
                return t.GetChild(i);
            }
        }
        return null;
    }
}
