using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviourSingleton<CameraController> {

    private Vector3 startPos;
    private Camera cam;

    private void Start()
    {
        startPos = transform.position;
        cam = GetComponent<Camera>();
        
        PlayerController.CloseToGround += ZoomIn;
        PlayerController.NotCloseToGround += ZoomOut;
    }

    public void ZoomIn(PlayerController p)
    {
        transform.position = new Vector3(p.transform.position.x,p.transform.position.y,transform.position.z);
        cam.orthographicSize = 2;
        Debug.Log("HOLA");

    }
    public void ZoomOut(PlayerController p)
    {
        transform.position = startPos;
        cam.orthographicSize = 5;
        Debug.Log("HOLA");
    }
}
