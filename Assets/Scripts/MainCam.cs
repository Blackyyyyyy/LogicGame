using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public static bool frozen = false;

    public float speed;
    public float zoomSpeed;

    private float camMinHeight = 0.9f;
    private float camMaxHeight = WorldSettings.size + 1;

    private void Start()
    {
        transform.position = new Vector3(WorldSettings.size / 2, WorldSettings.size / 2, -10);
    }

    void Update()
    {
        if (frozen) return;

        Vector3 transPos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(transPos.x + convertHorizontalMousePositionToDirection() * Time.deltaTime * speed, 0, WorldSettings.size), 
                                         Mathf.Clamp(transPos.y + convertVerticalMousePositionToDirection() * Time.deltaTime * speed, 0, WorldSettings.size),
                                         transPos.z);
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize + -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed, camMinHeight, camMaxHeight);
    }

    private int convertHorizontalMousePositionToDirection()
    {
        float mousePositionX = Input.mousePosition.x;
        if (/*mousePositionX < Screen.width * 0.02f || */Input.GetKey(Settings.left))
        {
            return -1; //left
        }
        else if (/*mousePositionX > Screen.width * 0.98f || */Input.GetKey(Settings.right))
        {
            return 1; //right
        }
        return 0; //idle
    }

    private int convertVerticalMousePositionToDirection()
    {
        float mousePositionY = Input.mousePosition.y;
        if(/*mousePositionY < Screen.height * 0.02f || */Input.GetKey(Settings.down))
        {
            return -1; //down
        }
        else if(/*mousePositionY > Screen.height * 0.98f || */Input.GetKey(Settings.up))
        {
            return 1; //up
        }
        return 0; //idle
    }
}
