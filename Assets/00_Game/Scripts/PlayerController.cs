using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourSingleton<PlayerController> {
    

    public float verticalForce = 1f;
    public float rotateForce = 1f;
    public float gravityScale = 0.1f;
    public float thresholdWin = 0.05f;    
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = gravityScale;
    }

    private void Update()
    {
        Inputs();        
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
