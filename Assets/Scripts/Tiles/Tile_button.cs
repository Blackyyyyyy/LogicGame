using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_button : TileData
{
    public string name { get; set; } = "button";
    public byte id { get; set; } = 6;
    public byte poweredID { get; set; } = 7;
    public string metadata { get; set; } = "0000";

    public Tile_button()
    {
        WorldSettings.tiles.Add(this);
    }

    public void setSignal(Tile tile, bool state)
    {
        tile.powered = state;
    }

    public void setSignalToAdjacentTile(Tile tile, bool state, int index)
    {
        tile.defaultSetSignalToAdjacentTile(state, index);
    }

    public void cycleMetaDataAt(int index)
    {
        this.defaultCycleMetaDataAt(index);
    }
}
