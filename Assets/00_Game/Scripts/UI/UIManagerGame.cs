﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerGame : MonoBehaviour {

    public GameObject UIGameCanvas;
    public GameObject UIPauseCanvas;
    public GameObject UIWin;
    public GameObject UILose;

    private void Start()
    {
        GameManager.ChangeScore += UpdateScoreInGame;
        GameManager.LevelWin += WinScreen;
        GameManager.LevelLose += LoseScreen;

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
        UIGameCanvas.SetActive(false);
        UIWin.SetActive(true);
    }
    public void LoseScreen(GameManager g)
    {
        UIGameCanvas.SetActive(false);
        UIWin.SetActive(true);
    }    
    private void UpdateScoreInGame(GameManager g)
    {
        UIGameCanvas.GetComponent<UIGame>().DrawScore(g.GetScore());
    }
    private void PauseGame(bool pauseOn)
    {
        if (pauseOn)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
