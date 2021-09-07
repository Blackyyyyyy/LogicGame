using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inentory : MonoBehaviour
{
    private Slot[] slots;
    public Slot slotPrefab;

    void Start()
    {
        slots = new Slot[WorldSettings.tiles.Count - 1];
        for(byte i = 1; i < WorldSettings.tiles.Count; i++)
        {
            slots[i - 1] = Instantiate(slotPrefab, transform);
            slots[i - 1].slotID = i;
            slots[i - 1].itemData = WorldSettings.tiles[i];
        }

        WorldSettings.slots = slots;
    }
}
