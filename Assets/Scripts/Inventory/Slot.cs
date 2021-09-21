using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool animate
    {
        get { return animateStorage; }
        set
        {
            animateStorage = value;
            if (!value)
            {
                transform.GetChild(0).localScale = new Vector2(1, 1);
                transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    private bool animateStorage = false;

    public byte slotID
    {
        get { return slotIDStorage; }
        set
        {
            GetComponentInChildren<Text>().text = value.ToString();
            slotIDStorage = value;
        }
    }
    private byte slotIDStorage;

    public TileData itemData
    {
        get { return itemDataStorage; }
        set
        {
            itemDataStorage = value;
            sprite = WorldSettings.textures[value.id];
            metadata = itemData.metadata;
        }
    }
    private TileData itemDataStorage;

    public string metadata
    {
        get { return metadataStorage; }
        set
        {
            metadataStorage = value;
            itemData.metadata = value;
            applyMetaData();
        }
    }
    private string metadataStorage = "0000";

    private Sprite sprite
    {
        get { return transform.GetChild(1).GetComponent<Image>().sprite; }
        set { transform.GetChild(1).GetComponent<Image>().sprite = value; }
    }

    private void Update()
    {
        if(animate)
        {
            float scale = Mathf.Sin(Time.time * 3) * 0.1f + 1f;
            transform.GetChild(0).localScale = new Vector2(scale, scale);
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Sin(Time.time * 6) * 0.5f + 0.8f);
        }
    }

    public void cycleMetaDataAt(int index)
    {
        itemData.cycleMetaDataAt(index);
        metadata = itemData.metadata;
    }

    private void applyMetaData()
    {
        for (int i = 0; i < 4; i++)
        {
            if (metadata.Substring(i, 1) != "0")
            {
                if (transform.getChildWithName(i.ToString()) != null) GameObject.Destroy(transform.getChildWithName(i.ToString()).gameObject);

                GameObject wire = new GameObject(i.ToString());

                wire.transform.parent = transform;
                wire.transform.localPosition = Vector3.zero;
                wire.transform.localScale = new Vector2(0.35f, 0.35f);

                wire.transform.rotation = Quaternion.Euler(0, 0, i * 90);

                if (metadata.Substring(i, 1) == "1") wire.AddComponent<Image>().sprite = WorldSettings.textures[4];
                else wire.AddComponent<Image>().sprite = WorldSettings.textures[12];
            }
            else if (metadata.Substring(i, 1) == "0" && transform.getChildWithName(i.ToString()) != null)
            {
                GameObject.Destroy(transform.getChildWithName(i.ToString()).gameObject);
            }
        }
    }
}