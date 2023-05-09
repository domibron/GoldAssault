using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public GameObject LoadingScreen;
    public ProgressBarManager ProgressBar;
    private bool isReloading = false;

    public bool loading = false;


    private void Awake()
    {
        current = this;

        LoadingScreen.SetActive(false);

        LoadMainMenu();

        // SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

    }

    private void Update()
    {
        isReloading = LoadingScreen.gameObject.activeSelf;
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void Loadlobby()
    {
        loading = true;
        LoadingScreen.gameObject.SetActive(true);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));

        if (SceneManager.sceneCount > 1)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        }

        scenesLoading.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadMainMenu()
    {
        loading = true;
        LoadingScreen.gameObject.SetActive(true);

        if (SceneManager.sceneCount > 1)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        }

        scenesLoading.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadMap(int indexNumber)
    {
        loading = true;
        LoadingScreen.gameObject.SetActive(true);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));

        if (SceneManager.sceneCount > 1)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        }

        scenesLoading.Add(SceneManager.LoadSceneAsync(indexNumber, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void Reload()
    {
        loading = true;
        if (isReloading) return;
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));

        LoadingScreen.gameObject.SetActive(true);

        Scene save = SceneManager.GetSceneAt(1);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        scenesLoading.Add(SceneManager.LoadSceneAsync(save.buildIndex, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                ProgressBar.current = totalSceneProgress;

                yield return null;
            }
        }

        loading = false;
        LoadingScreen.gameObject.SetActive(false);

        if (SceneManager.sceneCount > 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        }
    }
}