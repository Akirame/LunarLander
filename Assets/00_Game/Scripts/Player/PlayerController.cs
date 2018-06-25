using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourSingleton<PlayerController>
{

    public delegate void PlayerActions(PlayerController p);
    public delegate void LandedZone(LandZone l);
    public static PlayerActions CloseToGround;
    public static PlayerActions NotCloseToGround;
    public static PlayerActions BurnFuel;
    public static PlayerActions LandedFailed;
    public static LandedZone LandedSuccess;

    public Vector3 StartPos;
    public LayerMask layersZoom;
    public LayerMask layersLand;
    public float verticalForce = 1f;
    public float rotateForce = 1f;
    public float gravityScale = 0.1f;
    public float thresholdWin = 0.05f;    
    public float zoomDistance;
    public float fuelBurn = 1;
    public ParticleSystem particles;

    private Rigidbody2D rig;
    private float actualVel;

    private bool closerToGround;    
    private float rayLandDistance;
    private bool landOK;

    private float maxFuel;
    private float currFuel;

    private bool particlesOn;
    private Camera cam;
    private bool transitionOn = false;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        cam = CameraController.Get().GetViewPort();
        transform.position = StartPos;
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = gravityScale;
        maxFuel = 100;
        currFuel = maxFuel;
        closerToGround = false;        
        rayLandDistance = 0.51f;
        landOK = false;
        transitionOn = false;
        particlesOn = false;
    }
    private void OnEnable()
    {
        transform.position = StartPos;
        transform.eulerAngles = Vector3.zero;
    }
    public void ResetAll()
    {
        currFuel = maxFuel;
        particlesOn = false;
        landOK = false;
    }
    private void Update()
    {
        actualVel = rig.velocity.y;
        particlesOn = false;
        Inputs();
        CheckRaysZoom();
        CheckParticles();
        CheckOOB();
    }
    private void CheckOOB()
    {
        Vector3 actualPos = cam.WorldToViewportPoint(transform.position);
        if (actualPos.x < 0f || actualPos.x > 1f && !transitionOn)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transitionOn = true;
        }
        else
            transitionOn = false;

    }
    private void CheckRaysZoom()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -Vector2.up, zoomDistance, layersZoom);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right, zoomDistance, layersZoom);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.right, zoomDistance, layersZoom);
        if (hit1 || hit2 || hit3)
        {
            CloseToGround(this);
            closerToGround = true;
        }
        else if (closerToGround)
        {
            NotCloseToGround(this);
            closerToGround = false;
        }
    }
    private void CheckRaysLanding()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLandDistance, layersLand);
        if (hit1)
        {
            landOK = true;
        }
        else
            landOK = false;
    }
    private void Inputs()
    {
        if (Time.timeScale != 0 && currFuel > 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                if (!audio.isPlaying)
                    audio.Play();
                rig.AddForce(transform.up * verticalForce, ForceMode2D.Force);
                UpdateFuel();
                particlesOn = true;
            }
            else if (audio.isPlaying)
                audio.Stop();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateForce);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - rotateForce);
            }
        }
    }
    private void CheckParticles()
    {
        if (particlesOn)
            particles.Play();
        else
            particles.Stop();
    }
    private void UpdateFuel()
    {
        if (currFuel > 0)
            currFuel -= fuelBurn * Time.deltaTime;
        BurnFuel(this);
    }
    private void AddFuel()
    {
        currFuel += 10;
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
            LandedFailed(this);
        else if (collision.gameObject.tag == "LandZone")
        {
            CheckRaysLanding();
            if (actualVel >= -thresholdWin && actualVel <= thresholdWin && landOK)
            {
                AddFuel();
                LandedSuccess(collision.transform.GetComponent<LandZone>());
            }
            else
                LandedFailed(this);
        }
    }
}
