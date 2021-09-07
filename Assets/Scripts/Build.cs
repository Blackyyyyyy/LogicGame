using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    private Vector2 cursorPosition;
    
    private Slot activeSlot;

    void Start()
    {
        activeSlot = WorldSettings.slots[0];
        activeSlot.animate = true;
    }
    
    void Update()
    {
        cursorPosition = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        interactWithWorld();
        inventoryControls();
    }

    private void interactWithWorld()
    {
        if(Input.GetKeyDown(Settings.place) && cursorInBounds())
        {
            Vector2 tilePosition = convertCursorPositionToTilePosition();
            Tile currentTile = WorldSettings.world[(int)tilePosition.x, (int)tilePosition.y];

            if (currentTile.tileData.id == 0 ||
                (currentTile.tileData.id == activeSlot.itemData.id && currentTile.metadata != activeSlot.itemData.metadata))
            {
                currentTile.tileData = activeSlot.itemData;
            }
        }

        if(Input.GetKeyDown(Settings.remove) && cursorInBounds())
        {
            Vector2 tilePosition = convertCursorPositionToTilePosition();
            Tile currentTile = WorldSettings.world[(int)tilePosition.x, (int)tilePosition.y];

            if (currentTile.tileData.id != 0)
            {
                currentTile.tileData = WorldSettings.tiles[0];
            }
        }

        if(Input.GetKeyDown(Settings.edit) && cursorInBounds())
        {
            Vector2 tilePosition = convertCursorPositionToTilePosition();
            Tile currentTile = WorldSettings.world[(int)tilePosition.x, (int)tilePosition.y];
            if(currentTile.tileData.id == 6)
            {
                if (currentTile.powered) currentTile.removeSignal();
                else currentTile.receiveSignal();
            }
        }
    }

    private bool cursorInBounds()
    {
        return !(cursorPosition.x < -0.5 || cursorPosition.x > WorldSettings.size - 0.5 || cursorPosition.y < -0.5 || cursorPosition.y > WorldSettings.size - 0.5);
    }

    private Vector2 convertCursorPositionToTilePosition()
    {
        return new Vector2(Mathf.Round(cursorPosition.x), Mathf.Round(cursorPosition.y));
    }

    private void inventoryControls()
    {
        if (Input.GetKeyDown(Settings.slot_1))
        {
            if(WorldSettings.slots.Length > 0)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[0];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_2))
        {
            if (WorldSettings.slots.Length > 1)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[1];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_3))
        {
            if (WorldSettings.slots.Length > 2)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[2];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_4))
        {
            if (WorldSettings.slots.Length > 3)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[3];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_5))
        {
            if (WorldSettings.slots.Length > 4)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[4];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_6))
        {
            if (WorldSettings.slots.Length > 5)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[5];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_7))
        {
            if (WorldSettings.slots.Length > 6)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[6];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_8))
        {
            if (WorldSettings.slots.Length > 7)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[7];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_9))
        {
            if (WorldSettings.slots.Length > 8)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[8];
                activeSlot.animate = true;
            }
        }
        if (Input.GetKeyDown(Settings.slot_0))
        {
            if (WorldSettings.slots.Length > 9)
            {
                activeSlot.animate = false;
                activeSlot = WorldSettings.slots[9];
                activeSlot.animate = true;
            }
        }
    }
}
