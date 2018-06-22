using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour {

    public GameObject UIGameCanvas;
    public GameObject UIPauseCanvas;

    private void Start()
    {
        UIGameCanvas.SetActive(true);
        UIPauseCanvas.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        UIGameCanvas.SetActive(false);
        UIPauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIGameCanvas.SetActive(true);
        UIPauseCanvas.SetActive(false);
    }
}
