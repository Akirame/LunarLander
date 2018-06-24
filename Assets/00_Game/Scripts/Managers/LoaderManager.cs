using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public delegate void LoaderManagerActions();
    public static LoaderManagerActions NewLevel;
    public float loadingProgress;
    public float timeLoading;
    public float minTimeToLoad = 2;
    public string[] levels;    

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(AsynchronousLoad(sceneName));
    }
    public void LoadSceneWithoutLoading(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNewLevel()
    {
        NewLevel();
        SceneManager.LoadScene("LoadingScreen");
        string sceneName = levels[Random.Range(0, levels.Length)];
        StartCoroutine(AsynchronousLoad(sceneName));
    }
    IEnumerator AsynchronousLoad(string scene)
    {
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
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}