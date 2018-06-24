using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{

    public Image fuelBar;
    public Text altitudeText;
    public Text horizontalSpeed;
    public Text verticalSpeed;
    public Text timeText;
    public Text scoreText;

    private Vector3 shipPos;
    private Vector2 velocities;

    private void Start()
    {
        fuelBar.gameObject.SetActive(true);
        PlayerController.BurnFuel += DrawFuel;
        shipPos = PlayerController.Get().transform.position;
        velocities = PlayerController.Get().GetRigid().velocity;
        GameManager.ChangeScore += DrawScore;
    }
    private void Update()
    {
        DrawAltitude();
        DrawVelocities();
        DrawTime();
    }
    private void DrawVelocities()
    {
        if (velocities != PlayerController.Get().GetRigid().velocity)
        {
            velocities = PlayerController.Get().GetRigid().velocity;
            float velX = velocities.x * 10;
            float velY = velocities.y * 10;
            horizontalSpeed.text = "H: " + velX.ToString("0");
            verticalSpeed.text = "V: " + velY.ToString("0");
        }
    }
    private void DrawTime()
    {
        timeText.text = GameManager.Get().GetTime().ToString("000");
    }
    private void DrawAltitude()
    {
        if (shipPos != PlayerController.Get().transform.position)
        {
            shipPos = PlayerController.Get().transform.position;
            float altitude =  shipPos.y * 100;
            altitudeText.text = "Altitude: " + altitude.ToString("000") ;
        }
    }
    private void DrawFuel(PlayerController p)
    {
        fuelBar.fillAmount = p.GetCurrFuel();
    }
    private void DrawScore(int score)
    {        
        scoreText.text = score.ToString("000000");
    }
}
