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
        levelCount = 1;
        time = 0;
        PlayerController.LandedSuccess += WinLevel;
        PlayerController.LandedFailed += LoseLevel;
        LoaderManager.NewLevel += RaiseLevelCount;
        LoaderManager.NewLevel += DisableLevelPrefabs;
        LoaderManager.LoadCompleted += ActivateLevelPrefabs;
        LoaderManager.ReturnToMainMenu += DestroyPrefabs;
    }    
    private void Update()
    {
        time += Time.deltaTime;
    }
    public void DisableLevelPrefabs()
    {
        PlayerController.Get().transform.gameObject.SetActive(false);
        UIManagerGame.Get().transform.gameObject.SetActive(false);
    }
    public void ActivateLevelPrefabs()
    {
        PlayerController.Get().transform.gameObject.SetActive(true);
        UIManagerGame.Get().transform.gameObject.SetActive(true);
        CameraController.Get().ResetPos();
    }
    public void DestroyPrefabs()
    {
        Destroy(PlayerController.Get().transform.gameObject);
        Destroy(UIManagerGame.Get().transform.gameObject);
        CameraController.Get().ResetPos();
    }
    public void LoseLevel(PlayerController p)
    {
        LevelLose(this);
    }
    public void WinLevel(LandZone l)
    {                
        score += 100 * l.GetMultiplier();
        ChangeScore(this);
        LevelWin(this);
    }
    private void RaiseLevelCount()
    {
        levelCount++;
    }
    public int GetTime()
    {
        return Mathf.RoundToInt(time);
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
