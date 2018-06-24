using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandZone : MonoBehaviour {

    public Text mutiplierText;
    public int multiplier;

    private void Start()
    {
        mutiplierText.text = "x" + multiplier;
    }
    public int GetMultiplier()
    {
        return multiplier;
    }
}
