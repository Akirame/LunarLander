using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager> {

    public delegate void GameManagerActions(GameManager g);
    public static GameManagerActions ChangeScore;
    public static GameManagerActions LevelWin;
    public static GameManagerActions LevelLose;

    private int score;
    private float time;
    private int levelCount;

    private void Start()
    {
        levelCount = 0;
        time = 0;
        PlayerController.LandedSuccess += AddLandScore;
        LoaderManager.NewLevel += RaiseLevelCount;
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
        ChangeScore(this);
    }
    public int GetLevel()
    {
        return levelCount;
    }
    public int GetScore()
    {
        return score;
    }
    private void RaiseLevelCount()
    {
        levelCount++;
    }
}
