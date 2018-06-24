using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager> {

    public delegate void GameManagerActions(int score);
    public static GameManagerActions ChangeScore;

    private int score;
    private float time;
    private int levelCount;

    private void Start()
    {
        levelCount = 1;
        time = 0;
        PlayerController.Landed += AddLandScore;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    public int GetTime()
    {
        return Mathf.RoundToInt(time);
    }
    public void AddLandScore(LandZone l)
    {                
        score += 100 * l.GetMultiplier();
        ChangeScore(score);
    }
    public int GetLevel()
    {
        return levelCount;
    }
    public int GetScore()
    {
        return score;
    }
}
