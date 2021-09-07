using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public string name;
    public byte id;
    public byte poweredID;
    public string metadata;
}

public class Tile
{
    #region variables

    public TileData tileData
    {
        get { return tileDataStorage; }
        set
        {
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
                if(value) sprite = WorldSettings.textures[tileData.poweredID];
                else sprite = WorldSettings.textures[tileData.id];

                for (int i = 0; i < tileObject.childCount; i++)
                {
                    if(metadata.Substring(Convert.ToByte(tileObject.GetChild(i).name), 1) == "2") tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[12 + Convert.ToByte(value)];
                    else tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[4 + Convert.ToByte(value)];
                }
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
                if(getChildWithName(i.ToString()) != null) GameObject.Destroy(getChildWithName(i.ToString()).gameObject);

                GameObject wire = new GameObject(i.ToString());

                wire.transform.parent = tileObject;
                wire.transform.localPosition = Vector3.zero;

                wire.transform.rotation = Quaternion.Euler(0, 0, i * 90);

                if (metadata.Substring(i, 1) == "1") wire.AddComponent<SpriteRenderer>().sprite = WorldSettings.textures[4 + Convert.ToByte(powered)];
                else wire.AddComponent<SpriteRenderer>().sprite = WorldSettings.textures[12 + Convert.ToByte(powered)];
            }
            else if (metadata.Substring(i, 1) == "0" && getChildWithName(i.ToString()) != null)
            {
                GameObject.Destroy(getChildWithName(i.ToString()).gameObject);
            }
        }
    }

    public void setSignal(bool state)
    {
        if (powered == state) return;

        switch(tileData.id)
        {
            case 8:
                if (state != Convert.ToBoolean(Convert.ToByte(metadata.Substring(5, 1)))) LogicEngine.current.queueRepeater(this, state);
                break;
            default:
                powered = state;
                break;
        }
    }

    private void setSignalToAdjacentTiles(bool state)
    {
        for(int i = 0; i < 4; i++)
        {
            Tile adjacentTile = getAdjacentTile(i);
            if (adjacentTile == null) continue;
            switch(tileData.id)
            {
                case 8:
                    if (metadata.Substring(i, 1) == "2" && adjacentTile.metadata.Substring(getOppositeIndex(i), 1) == "1")
                    {
                        adjacentTile.setSignal(state);
                    }
                    break;
                default:
                    if (metadata.Substring(i, 1) == "1" && adjacentTile.metadata.Substring(getOppositeIndex(i), 1) == "1")
                    {
                        adjacentTile.setSignal(state);
                    }
                    break;
            }
        }
    }

    private int getOppositeIndex(int index)
    {
        if (index < 2) return index + 2;
        else return index - 2;
    }

    private Tile getAdjacentTile(int index)
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

    #region Children
    private Transform getChildWithName(string index)
    {

        for (int i = 0; i < tileObject.childCount; i++)
        {
            if (tileObject.GetChild(i).name == index)
            {
                return tileObject.GetChild(i);
            }
        }
        return null;
    }
    #endregion
}
