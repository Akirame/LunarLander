using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviourSingleton<Loader> {

    public void ChangeLevel()
    {
        LoaderManager.Get().LoadNewLevel();
    }
    public void ToMainMenu()
    {
        LoaderManager.Get().LoadMainMenu();
    }
}
