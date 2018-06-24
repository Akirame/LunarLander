using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviourSingleton<Loader> {

    public void ChangeLevel(bool firstLevel)
    {
        LoaderManager.Get().LoadNewLevel(firstLevel);
    }
    public void ToMainMenu()
    {
        LoaderManager.Get().LoadMainMenu();
    }
}
