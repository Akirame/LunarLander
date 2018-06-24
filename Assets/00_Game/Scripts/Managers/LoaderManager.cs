using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public delegate void LoaderManagerActions();
    public static LoaderManagerActions NewLevel;
    public static LoaderManagerActions LoadCompleted;
    public static LoaderManagerActions ReturnToMainMenu;

    public float loadingProgress;
    public float timeLoading;
    public float minTimeToLoad = 2;
    public string[] levels;
    private bool firstLevel;
    private bool loading = false;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(AsynchronousLoad(sceneName));
    }
    public void LoadMainMenu()
    {
        ReturnToMainMenu();
        SceneManager.LoadScene("00_MainMenu");
    }
    public void LoadNewLevel()
    {                
        if(!GameManager.Get().FirstLoad())
        NewLevel();
        SceneManager.LoadScene("LoadingScreen");
        Debug.Log(levels.Length);
        string sceneName = levels[Random.Range(0,levels.Length)];
        if(!loading)
        StartCoroutine(AsynchronousLoad(sceneName));        
    }
    IEnumerator AsynchronousLoad(string scene)
    {
        loading = true;
        loadingProgress = 0;
        timeLoading = 0;
        yield return null;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            timeLoading += Time.deltaTime;
            loadingProgress = ao.progress + 0.1f;
            loadingProgress = loadingProgress * timeLoading / minTimeToLoad;

            // Loading completed
            if (loadingProgress >= 1)
            {
                if (!GameManager.Get().FirstLoad())
                    LoadCompleted();                                    
                ao.allowSceneActivation = true;
            }
            loading = false;
            yield return null;
        }
    }
}