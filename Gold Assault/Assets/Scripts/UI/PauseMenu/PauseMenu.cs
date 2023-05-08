using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject Pausemenu;
    public GameObject SettingMenu;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Pausemenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0;

            if (!PauseMenuUI.activeSelf)
            {
                PauseMenuUI.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;

            if (PauseMenuUI.activeSelf)
            {
                PauseMenuUI.SetActive(false);
            }

            if (SettingMenu.activeSelf)
            {
                SettingMenu.SetActive(false);
            }

            if (!Pausemenu.activeSelf)
            {
                Pausemenu.SetActive(true);
            }
        }

    }

    public void GoBack()
    {
        SettingMenu.SetActive(false);
        Pausemenu.SetActive(true);
    }

    public void OpenSettings()
    {
        SettingMenu.SetActive(true);
        Pausemenu.SetActive(false);
    }

    public void ReturnToLobby()
    {
        GameManager.current.Loadlobby();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}