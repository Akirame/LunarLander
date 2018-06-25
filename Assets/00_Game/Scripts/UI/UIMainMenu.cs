using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour {

    public GameObject canvasMenu;
    public GameObject CanvasCredits;

    private void Start()
    {
        canvasMenu.SetActive(true);
        CanvasCredits.SetActive(false);
    }
    public void ActivateMenu()
    {
        canvasMenu.SetActive(true);
        CanvasCredits.SetActive(false);
    }
    public void ActivateCredits()
    {
        canvasMenu.SetActive(false);
        CanvasCredits.SetActive(true);
    }
    public void ExitGame()
    {        
        Application.Quit();
    }    
}
