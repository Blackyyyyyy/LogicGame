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
            transform.GetChild(1).GetComponent<Image>().sprite = WorldSettings.textures[value.id];
            itemDataStorage = value;
        }
    }
    private TileData itemDataStorage;

    private void Update()
    {
        if(animate)
        {
            float scale = Mathf.Sin(Time.time * 3) * 0.1f + 1f;
            transform.GetChild(0).localScale = new Vector2(scale, scale);
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Sin(Time.time * 6) * 0.5f + 0.8f);
        }
    }
}
