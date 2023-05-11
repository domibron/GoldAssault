using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool OveridingEscape = false;

    public bool OveridingTimeScale = false;

    public GameObject PauseMenuUI;
    public GameObject Pausemenu;
    public GameObject SettingMenu;

    private bool isPaused = false;

    private bool runOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        Pausemenu.SetActive(true);
        SettingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !OveridingEscape)
        {
            isPaused = !isPaused;
        }

        if (OveridingEscape)
        {
            isPaused = false;
        }


        if (isPaused && !OveridingTimeScale)
        {
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            runOnce = false;

            if (!PauseMenuUI.activeSelf)
            {
                PauseMenuUI.SetActive(true);
            }
        }
        else if (!OveridingTimeScale)
        {
            Time.timeScale = 1;

            if (!runOnce)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

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

    public void SaveSettings()
    {

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

    public void ResumeGame()
    {
        isPaused = false;
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
