using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class WorldSettings
{
    public static List<TileData> tiles;
    public static List<Sprite> textures;

    public static Tile[,] world;
    public static Slot[] slots;

    public static byte size = 50;
    public static byte pixelPerTile = 16;
}
