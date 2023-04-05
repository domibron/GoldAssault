using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject LevelSelector;

    // Start is called before the first frame update
    void Start()
    {
        LevelSelector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMap()
    {
        LevelSelector.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseMap()
    {
        LevelSelector.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
