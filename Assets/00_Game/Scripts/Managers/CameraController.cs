using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviourSingleton<CameraController> {

    public int zoomOutInt = 10;
    public int ZoomInInt = 2;
    public Vector3 startPos;

    private Camera cam;

    private void Start()
    {
        transform.position = startPos;
        cam = GetComponent<Camera>();

        PlayerController.CloseToGround += ZoomIn;
        PlayerController.NotCloseToGround += ZoomOut;
    }

    public void ZoomIn(PlayerController p)
    {
        transform.position = new Vector3(p.transform.position.x, p.transform.position.y, transform.position.z);
        cam.orthographicSize = ZoomInInt;

    }
    public void ZoomOut(PlayerController p)
    {
        transform.position = startPos;
        cam.orthographicSize = zoomOutInt;
    }
    public void ResetPos()
    {
        transform.position = startPos;
        cam.orthographicSize = zoomOutInt;
    }
    public Camera GetViewPort()
    {
        return cam;
    }
}
