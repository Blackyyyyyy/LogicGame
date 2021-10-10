using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface TileData
{
    string name { get; set; }
    byte id { get; set; }
    byte poweredID { get; set; }
    string metadata { get; set; } //up, left, down, right

    void setSignal(Tile tile, bool state);
    void setSignalToAdjacentTile(Tile tile, bool state, int index);
    void cycleMetaDataAt(int index);
}

public class Tile
{
    #region variables

    public TileData tileData
    {
        get { return tileDataStorage; }
        set
        {
            powered = false;
            tileDataStorage = value;
            sprite = WorldSettings.textures[value.id];
            metadata = value.metadata;
        }
    }
    private TileData tileDataStorage;

    public string metadata
    {
        get { return metadataStorage; }
        set
        {
            metadataStorage = value;
            applyMetaData();
        }
    }
    private string metadataStorage = "0000";
    

    private Sprite sprite
    {
        get { return tileObject.GetComponent<SpriteRenderer>().sprite; }
        set { tileObject.GetComponent<SpriteRenderer>().sprite = value; }
    }
    private Transform tileObject;

    public bool powered
    {
        get { return poweredStorage; }
        set
        {
            if (value != powered)
            {
                //display stuff
                if(value) sprite = WorldSettings.textures[tileData.poweredID];
                else sprite = WorldSettings.textures[tileData.id];

                for (int i = 0; i < tileObject.childCount; i++)
                {
                    if(metadata.Substring(Convert.ToByte(tileObject.GetChild(i).name), 1) == "2") tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[12 + Convert.ToByte(value)];
                    else tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[4 + Convert.ToByte(value)];
                }

                //tech stuff
                poweredStorage = value;
                setSignalToAdjacentTiles(value);
            }
        }
    }
    private bool poweredStorage;

    #endregion


    public Tile(TileData tileData, Transform tileObject)
    {
        this.tileObject = tileObject;
        this.tileData = tileData;
    }

    public Tile(TileData tileData, Transform tileObject, string metadata)
    {
        this.tileObject = tileObject;
        this.tileData = tileData;
        this.metadata = metadata;
    }

    private void applyMetaData()
    {
        for (int i = 0; i < 4; i++)
        {
            if (metadata.Substring(i, 1) != "0")
            {
                if(tileObject.getChildWithName(i.ToString()) != null) GameObject.Destroy(tileObject.getChildWithName(i.ToString()).gameObject);

                GameObject wire = new GameObject(i.ToString());

                wire.transform.parent = tileObject;
                wire.transform.localPosition = Vector3.zero;

                wire.transform.rotation = Quaternion.Euler(0, 0, i * 90);

                if (metadata.Substring(i, 1) == "1") wire.AddComponent<SpriteRenderer>().sprite = WorldSettings.textures[4 + Convert.ToByte(powered)];
                else wire.AddComponent<SpriteRenderer>().sprite = WorldSettings.textures[12 + Convert.ToByte(powered)];
            }
            else if (metadata.Substring(i, 1) == "0" && tileObject.getChildWithName(i.ToString()) != null)
            {
                GameObject.Destroy(tileObject.getChildWithName(i.ToString()).gameObject);
            }
        }
    }

    public void setSignal(bool state)
    {
        tileData.setSignal(this, state);
    }

    private void setSignalToAdjacentTiles(bool state)
    {
        for(int i = 0; i < 4; i++)
        {
            tileData.setSignalToAdjacentTile(this, state, i);
        }
    }

    public void defaultSetSignalToAdjacentTile(bool state, int index)
    {
        Tile adjacentTile = getAdjacentTile(index);
        if (adjacentTile == null) return;

        if (metadata.Substring(index, 1) != "0" && adjacentTile.metadata.Substring(getOppositeIndex(index), 1) == "1") adjacentTile.setSignal(state);
        else if(state == false && adjacentTile.powered && metadata.Substring(index, 1) == "1" && adjacentTile.metadata.Substring(getOppositeIndex(index), 1) == "2")
        {
            Debug.Log("Yo. Fix this. defaultSetSignalToAdjacentTile");
            //setSignal(true);
        }
    }

    public Tile getAdjacentTile(int index)
    {
        int x = (int)tileObject.position.x;
        int y = (int)tileObject.position.y;

        switch (index)
        {
            case 0:
                if (y == WorldSettings.size - 1) return null;
                return WorldSettings.world[x, y + 1];
            case 1:
                if (x == 0) return null;
                return WorldSettings.world[(int)tileObject.position.x - 1, (int)tileObject.position.y];
            case 2:
                if (y == 0) return null;
                return WorldSettings.world[(int)tileObject.position.x, (int)tileObject.position.y - 1];
            case 3:
                if (x == WorldSettings.size - 1) return null;
                return WorldSettings.world[(int)tileObject.position.x + 1, (int)tileObject.position.y];
            default:
                return null;
        }
    }

    public int getOppositeIndex(int index)
    {
        if (index < 2) return index + 2;
        else return index - 2;
    }
}
