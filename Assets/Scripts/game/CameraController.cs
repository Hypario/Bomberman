using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera cam;
    private float wantedZoom;
    private float zoomFactor = 1.5f;
    private float zoomSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        wantedZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll;
        scroll = Input.GetAxis("Mouse ScrollWheel");

        wantedZoom -= scroll * zoomFactor;
        wantedZoom = Mathf.Clamp(wantedZoom, 2f, 5f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, wantedZoom, Time.deltaTime * zoomSpeed);
    }
}
