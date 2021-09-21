using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_blank : TileData
{
    public string name { get; set; } = "blank";
    public byte id { get; set; } = 1;
    public byte poweredID { get; set; } = 1;
    public string metadata { get; set; } = "0000";

    public Tile_blank()
    {
        WorldSettings.tiles.Add(this);
    }

    public void setSignal(Tile tile, bool state)
    {
        tile.powered = state;
    }

    public void setSignalToAdjacentTile(Tile tile, bool state, int index) { }

    public void cycleMetaDataAt(int index) { }
}
