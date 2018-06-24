using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager> {

    private float score;
    private float time;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    public int GetTime()
    {
        return Mathf.RoundToInt(time);
    }
}
