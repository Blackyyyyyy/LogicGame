using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_lamp : TileData
{
    public string name { get; set; } = "lamp";
    public byte id { get; set; } = 10;
    public byte poweredID { get; set; } = 11;
    public string metadata { get; set; } = "0000";

    public Tile_lamp()
    {
        WorldSettings.tiles.Add(this);
    }

    public void setSignal(Tile tile, bool state)
    {
        if (tile.powered == state) return;

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
