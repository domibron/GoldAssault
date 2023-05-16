using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private MasterManger MM;

    public GameObject[] menus;

    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        LoadMenu(0); // loads the loading menu

        if (MasterManger.current != null) // maybe try a try catch. this is to prevent a game crash and stop the game from proceeding.
        {
            MM = MasterManger.current;
        }
        else
        {
            Debug.LogError("CRITICAL ERROR - Failed to start up the master manager. Game forced to close");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MM.loaded == true && menus[0].activeSelf)
        {
            LoadMenu(1);
        }
    }

    public void StartGame()
    {
        if (GameManager.current != null)
            GameManager.current.Loadlobby();
        else
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu(int menuNum) // cool menu loader. Users the 0 - max not 1 to max.
    {
        for (int i = 0; i < menus.Length; i++)
        {
            print(i);
            if (i == menuNum)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }
    }

    public void ChangeWindowMode()
    {
        if (dropdown.value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (dropdown.value == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

        }
        else if (dropdown.value == 2)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;

        }
        else if (dropdown.value == 3)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;

        }
    }

}
