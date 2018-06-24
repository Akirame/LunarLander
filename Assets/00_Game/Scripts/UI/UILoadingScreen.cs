using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UILoadingScreen : MonoBehaviour
{
    public Text loadingText;
    public Text nextLevel;
    public Text funnyText;

    private bool textChange;
    private int levelCount;

    public void SetVisible(bool show)
    {
        levelCount = GameManager.Get().GetLevel();
        textChange = false;
        gameObject.SetActive(show);
    }    

    public void Update()
    {
        int loadingVal = (int)(LoaderManager.Get().loadingProgress * 100);
        loadingText.text = "Loading " + loadingVal;

        if (LoaderManager.Get().loadingProgress >= 1)
            Destroy(this.gameObject);

        if(!textChange)
        StartCoroutine(RandomTexts());

        nextLevel.text = "Next zone: " + levelCount;
    }
    private IEnumerator RandomTexts()
    {        
        textChange = true;
        switch (Random.Range(0,4))
        {
            case 0:
                funnyText.text = "Creating Rocks";
                break;
            case 1:
                funnyText.text = "Recharging Fuel";
                break;
            case 2:
                funnyText.text = "Calibrating";
                break;
            case 3:
                funnyText.text = "Fighting aliens";
                break;
        }
        yield return new WaitForSeconds(1);
        textChange = false;
    }
}