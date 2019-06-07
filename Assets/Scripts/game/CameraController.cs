using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
        float screenRatio = (float)Screen.width / (float)Screen.height; // calculate the ratio of the user screen
        float targetRatio = (float)tilemap.size.x / (float)tilemap.size.y; // calculate the ratio needed for the map

        if (screenRatio >= targetRatio) // if the ratio is bigger than the one needed
        {
            Camera.main.orthographicSize = tilemap.size.y / 2; // the camera take the height of the map as size
        } else
        {
            float difference = targetRatio / screenRatio; // else we calculate the difference beetwen them
            Camera.main.orthographicSize = tilemap.size.y / 2 * difference; 
            // the camera take the height of the map as a size, multiplied by the difference (as we need more space)
        }
    }
}
