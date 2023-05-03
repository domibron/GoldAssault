using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMBackup : MonoBehaviour
{
    private int index;

    void Awake()
    {
        if (GameManager.current == null)
        {
            DontDestroyOnLoad(this);

            Debug.LogWarning($"You did not load into {SceneManager.GetSceneAt(0).name} {SceneManager.GetSceneAt(0).buildIndex} correctly!\nLoading into Game Manager scene now.");
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            index = SceneManager.GetSceneAt(0).buildIndex;
            StartCoroutine(WaitAndReloadMap());
        }
    }


    IEnumerator WaitAndReloadMap()
    {
        //yield return new WaitForSeconds(1f);
        while (GameManager.current == null)
        {
            yield return null;
        }
        while (GameManager.current.loading == true)
        {
            yield return null;
        }

        // yield return new WaitForSeconds(0.1f);

        GameManager.current.LoadMap(index);
        // Destroy(this);
        yield return true;
    }
}
