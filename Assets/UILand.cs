using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILand : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        UpdateText();
    }
    private void OnEnable()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        scoreText.text = "Score\n" + GameManager.Get().GetScore().ToString("000000000");
    }
}
