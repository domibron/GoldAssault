using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager current { get; private set; }

    public string saveName = "0";

    public event Action onSave;
    public void GameSave()
    {
        if (onSave != null)
        {
            onSave();
        }
    }

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this);
        }

        SerializationManager.Load(Application.persistentDataPath + "/saves/0.save");
    }

    public void OnSave()
    {
        SerializationManager.Save(saveName, SaveData.current);
        GameSave();
    }

    // public string[] saveFiles;
    // public void GetLoadFiles()
    // {
    //     if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
    //     {
    //         Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
    //     }

    //     saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    // }

    public int[] getCurrentInventory()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/" + "0.save"))
        {
            //Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + "0.save");
            print("no");
            //SerializationManager.Save("0", SaveData.current);
            //return null;
            print(SaveData.current.inventory);
        }

        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/0.save");

        return SaveData.current.inventory;
    }

    //public static int[] _tempInv = { 0, 0, 0, 0, 0 };

    // public void ShowLoadScreen()
    // {
    //     GetLoadFiles();

    //     foreach (Transform button in loadArea)
    //     {
    //         Destroy(button.gameObject);
    //     }

    //     for (int i = 0; i < saveFiles.Length; i++)
    //     {
    //         GameObject buttonObject = Instantiate(buttonPrefab);
    //         buttonObject.transform.SetParent(loadArea.transform, false);

    //         var index = i;
    //         buttonObject.GetComponent<Button>().onClick.AddListener(() =>
    //         {
    //             // load data
    //         });
    //         buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index].Replace(Application.persistentDataPath + "/saves/", "");
    //     }

    // }
}
