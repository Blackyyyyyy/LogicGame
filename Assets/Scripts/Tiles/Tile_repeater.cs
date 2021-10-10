using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_repeater : TileData
{
    public string name { get; set; } = "repeater";
    public byte id { get; set; } = 8;
    public byte poweredID { get; set; } = 9;
    public string metadata { get; set; } = "00009";

    public Tile_repeater()
    {
        WorldSettings.tiles.Add(this);
    }

    public void setSignal(Tile tile, bool state)
    {
        /*if (state != Convert.ToBoolean(Convert.ToByte(tile.metadata.Substring(5, 1))))*/ LogicEngine.current.queueRepeater(tile, state);
    }

    public void setSignalToAdjacentTile(Tile tile, bool state, int index)
    {
        Tile adjacentTile = tile.getAdjacentTile(index);
        if (adjacentTile == null) return;

        if (tile.metadata.Substring(index, 1) == "2" && adjacentTile.metadata.Substring(tile.getOppositeIndex(index), 1) == "1")
        {
            adjacentTile.setSignal(state);
        }
    }

    public void cycleMetaDataAt(int index)
    {
        if (metadata.getCharAt(index) == '0') metadata = metadata.setCharAt(index, '1');
        else if (metadata.getCharAt(index) == '1') metadata = metadata.setCharAt(index, '2');
        else metadata = metadata.setCharAt(index, '0');
    }
}
