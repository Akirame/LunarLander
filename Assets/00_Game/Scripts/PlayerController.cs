using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourSingleton<PlayerController> {

    public delegate void PlayerActions(PlayerController p);
    public static PlayerActions CloseToGround;
    public static PlayerActions NotCloseToGround;
    public static PlayerActions BurnFuel;

    public LayerMask layers;
    public float verticalForce = 1f;
    public float rotateForce = 1f;
    public float gravityScale = 0.1f;
    public float thresholdWin = 0.05f;    
    public float zoomDistance;
    public float fuelBurn = 1;

    private Rigidbody2D rig;
    private bool closer;
    private float maxFuel;
    private float currFuel;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = gravityScale;
        closer = false;
        maxFuel = 100;
        currFuel = maxFuel;        
    }

    private void Update()
    {
        Inputs();
        CheckRays();
    }
    private void CheckRays()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -Vector2.up, zoomDistance, layers);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right, zoomDistance, layers);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.right, zoomDistance, layers);
        if (hit1 || hit2 || hit3)
        {
            CloseToGround(this);
            closer = true;
        }
        else if (closer)
        {
            NotCloseToGround(this);
            closer = false;
        }
    }
    private void Inputs()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            rig.AddForce(transform.up * verticalForce, ForceMode2D.Force);
            UpdateFuel();
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
    private void UpdateFuel()
    {
        if(currFuel > 0)
        currFuel -= fuelBurn * Time.deltaTime;        
        BurnFuel(this);
    }
    public Rigidbody2D GetRigid()
    {
        return GetComponent<Rigidbody2D>();
    }
    public float GetCurrFuel()
    {                
        return (currFuel / 100);
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
