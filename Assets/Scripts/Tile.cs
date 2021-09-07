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
    #region idunno

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
            if (value && !powered)
            {
                sprite = WorldSettings.textures[tileData.poweredID];

                for (int i = 0; i < tileObject.childCount; i++)
                {
                    tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[5];
                }
                poweredStorage = value;
                sendSignalToAdjacentTiles();
            }
            else if(!value && powered)
            {
                sprite = WorldSettings.textures[tileData.id];

                for (int i = 0; i < tileObject.childCount; i++)
                {
                    tileObject.GetChild(i).GetComponent<SpriteRenderer>().sprite = WorldSettings.textures[4];
                }
                poweredStorage = value;
                removeSignalFromAdjacentTiles();
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
            if (metadata.Substring(i, 1).Equals("1") && getChildWithName(i.ToString()) == null)
            {
                GameObject wire = new GameObject();
                wire.name = i.ToString();

                wire.transform.parent = tileObject;
                wire.transform.localPosition = Vector3.zero;

                wire.transform.rotation = Quaternion.Euler(0, 0, i * 90);
                wire.AddComponent<SpriteRenderer>().sprite = WorldSettings.textures[4];
            }
            else if (metadata.Substring(i, 1).Equals("0") && getChildWithName(i.ToString()) != null)
            {
                Object.Destroy(getChildWithName(i.ToString()).gameObject);
            }
        }
    }

    public void receiveSignal()
    {
        if (powered) return;
        powered = true;

        switch(tileData.id)
        {
            case 8:
                break;
            default:
                break;
        }
    }

    public void removeSignal()
    {
        if (!powered) return;
        powered = false;

        switch (tileData.id)
        {
            case 8:
                break;
            default:
                break;
        }
    }

    private void sendSignalToAdjacentTiles()
    {
        for(int i = 0; i < 4; i++)
        {
            Tile adjacentTile = getAdjacentTile(i);
            if (adjacentTile == null) continue;
            if (metadata.Substring(i, 1).Equals("1") && adjacentTile.metadata.Substring(getOppositeIndex(i), 1).Equals("1"))
            {
                adjacentTile.receiveSignal();
            }
        }
    }

    private void removeSignalFromAdjacentTiles()
    {
        for (int i = 0; i < 4; i++)
        {
            Tile adjacentTile = getAdjacentTile(i);
            if (adjacentTile == null) continue;
            if (metadata.Substring(i, 1).Equals("1") && adjacentTile.metadata.Substring(getOppositeIndex(i), 1).Equals("1"))
            {
                adjacentTile.removeSignal();
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
            if (tileObject.GetChild(i).name.Equals(index))
            {
                return tileObject.GetChild(i);
            }
        }
        return null;
    }
    #endregion
}
