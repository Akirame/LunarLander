using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerGame : MonoBehaviourSingleton<UIManagerGame> {

    public GameObject UIGameCanvas;
    public GameObject UIPauseCanvas;
    public GameObject UIWin;
    public GameObject UILose;

    private void Start()
    {
        GameManager.ChangeScore += UpdateScoreInGame;
        GameManager.LevelWin += UpdateScoreInGame;
        GameManager.LevelWin += WinScreen;
        GameManager.LevelLose += LoseScreen;
        LoaderManager.LoadCompleted += Init;
        Init();
    }
    public void Init()
    {
        UIGameCanvas.SetActive(true);
        UIPauseCanvas.SetActive(false);
        UIWin.SetActive(false);
        UILose.SetActive(false);
    }
    public void PauseScreen()
    {
        PauseGame(true);
        UIGameCanvas.SetActive(false);
        UIPauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        PauseGame(false);
        UIGameCanvas.SetActive(true);
        UIPauseCanvas.SetActive(false);
    }
    public void WinScreen(GameManager g)
    {
        PauseGame(true);
        UIGameCanvas.SetActive(false);
        UIWin.SetActive(true);
    }
    public void LoseScreen(GameManager g)
    {
        PauseGame(true);
        UIGameCanvas.SetActive(false);
        UILose.SetActive(true);
    }    
    private void UpdateScoreInGame(GameManager g)
    {
        UIGameCanvas.GetComponent<UIGame>().DrawScore(g.GetScore());
    }
    public void PauseGame(bool pauseOn)
    {
        if (pauseOn)
        {
            Time.timeScale = 0;            
        }
        else
        {
            Time.timeScale = 1;            
        }
    }
}
