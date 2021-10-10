using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_wire : TileData
{
    public string name { get; set; } = "wire";
    public byte id { get; set; } = 2;
    public byte poweredID { get; set; } = 3;
    public string metadata { get; set; } = "0000";

    public Tile_wire()
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
