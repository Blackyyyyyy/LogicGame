using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEngine : MonoBehaviour
{
    public static LogicEngine current;

    void Start()
    {
        current = this;
    }

    public void queueRepeater(Tile repeater, bool state)
    {
        byte delayInFrames = Convert.ToByte(repeater.metadata.Substring(4, 1));
        //repeater.metadata = repeater.metadata.Substring(0, 5) + Convert.ToByte(state);
        StartCoroutine(queueSignalChange(repeater, delayInFrames, state));
    }

    private IEnumerator queueSignalChange(Tile tile, byte delayInFrames, bool state)
    {
        for(byte i = 0; i < delayInFrames; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        tile.powered = state;
    }
}
