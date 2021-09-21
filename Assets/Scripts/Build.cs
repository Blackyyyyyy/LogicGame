using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    private Vector2 cursorPosition;
    
    private Slot activeSlot;
    private bool editingSelectedTile = false;

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
                currentTile.setSignal(!currentTile.powered);
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

    private void setActiveSlot(int index)
    {
        if(WorldSettings.slots.Length > index)
        {
            activeSlot.animate = false;
            activeSlot = WorldSettings.slots[index];
            activeSlot.animate = true;
        }
    }

    private void inventoryControls()
    {
        if (Input.GetKeyDown(Settings.slot_1))
        {
            setActiveSlot(0);
        }
        if (Input.GetKeyDown(Settings.slot_2))
        {
            setActiveSlot(1);
        }
        if (Input.GetKeyDown(Settings.slot_3))
        {
            setActiveSlot(2);
        }
        if (Input.GetKeyDown(Settings.slot_4))
        {
            setActiveSlot(3);
        }
        if (Input.GetKeyDown(Settings.slot_5))
        {
            setActiveSlot(4);
        }
        if (Input.GetKeyDown(Settings.slot_6))
        {
            setActiveSlot(5);
        }
        if (Input.GetKeyDown(Settings.slot_7))
        {
            setActiveSlot(6);
        }
        if (Input.GetKeyDown(Settings.slot_8))
        {
            setActiveSlot(7);
        }
        if (Input.GetKeyDown(Settings.slot_9))
        {
            setActiveSlot(8);
        }
        if (Input.GetKeyDown(Settings.slot_0))
        {
            setActiveSlot(9);
        }

        if(!editingSelectedTile && Input.GetKeyDown(Settings.editSelectedTile))
        {
            MainCam.frozen = true;
            editingSelectedTile = true;
        }
        else if(editingSelectedTile && Input.GetKeyUp(Settings.editSelectedTile))
        {
            MainCam.frozen = false;
            editingSelectedTile = false;
        }

        if(editingSelectedTile)
        {
            if(Input.GetKeyDown(Settings.up))
            {
                activeSlot.cycleMetaDataAt(0);
            }
            if (Input.GetKeyDown(Settings.left))
            {
                activeSlot.cycleMetaDataAt(1);
            }
            if (Input.GetKeyDown(Settings.down))
            {
                activeSlot.cycleMetaDataAt(2);
            }
            if (Input.GetKeyDown(Settings.right))
            {
                activeSlot.cycleMetaDataAt(3);
            }
        }
    }
}
