using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour {

    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;

    public float vel1;
    public float vel2;
    public float vel3;
    public float vel4;

    private Rigidbody2D RigidShip;

    private void Start()
    {
        RigidShip = PlayerController.Get().GetRigid();
    }
    private void Update()
    {
            img1.rectTransform.anchoredPosition += new Vector2(vel1 * Time.deltaTime * RigidShip.velocity.x, 0);
            img3.rectTransform.anchoredPosition += new Vector2(vel3 * Time.deltaTime * RigidShip.velocity.x, 0);
            img2.rectTransform.anchoredPosition += new Vector2(vel2 * Time.deltaTime * RigidShip.velocity.x, 0);
            img4.rectTransform.anchoredPosition += new Vector2(vel4 * Time.deltaTime * RigidShip.velocity.x, 0);     
    }
}
