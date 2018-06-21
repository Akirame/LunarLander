using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourSingleton<PlayerController> {

    public delegate void PlayerActions(PlayerController p);
    public static PlayerActions CloseToGround;
    public static PlayerActions NotCloseToGround;
    public LayerMask layers;
    public float verticalForce = 1f;
    public float rotateForce = 1f;
    public float gravityScale = 0.1f;
    public float thresholdWin = 0.05f;    
    public float zoomDistance;

    private Rigidbody2D rig;
    private bool closer;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = gravityScale;
        closer = false;       
    }

    private void Update()
    {
        Inputs();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, zoomDistance, layers);
        if (hit)
        {
                CloseToGround(this);
                closer = true;
        }
        else if (closer)
        {
            NotCloseToGround(this);
            closer = false;
            Debug.Log("HOL");

        }
        Debug.DrawRay(transform.position, -Vector3.up * zoomDistance);
    }
    private void Inputs()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            rig.AddForce(transform.up * verticalForce, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateForce);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotateForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tiles")
            if (rig.velocity.y >= -thresholdWin && rig.velocity.y <= thresholdWin)
                Debug.Log("Win");
            else
                Debug.Log("bum");
    }
}
