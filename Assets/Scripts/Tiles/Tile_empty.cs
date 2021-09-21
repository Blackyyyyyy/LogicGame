using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_empty : TileData
{
    public string name { get; set; } = "empty";
    public byte id { get; set; } = 0;
    public byte poweredID { get; set; } = 0;
    public string metadata { get; set; } = "0000";

    public Tile_empty()
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
