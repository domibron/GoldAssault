using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
// ! ===============================
using UnityEditor;

[CustomEditor(typeof(OverideSaveData)), CanEditMultipleObjects]
public class OverideSaveDataButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OverideSaveData myScript = (OverideSaveData)target;
        if (GUILayout.Button("Force Save"))
            myScript.saveArrayToInventory();
    }
}
// ! ===============================
#endif

public class OverideSaveData : MonoBehaviour
{
    [SerializeField] public int[] _temp = new int[5];

    void Start()
    {
        _temp = SaveData.current.inventory;

        SaveManager.current.onSave += OnSaveGame;
    }

    public void OnSaveGame()
    {
        print("loaded save inv");

        _temp = SaveData.current.inventory;
    }

    public void saveArrayToInventory()
    {
        ModifyInventory(_temp);
    }

    public void ModifyInventory(int[] arrayForInventory)
    {
        if (arrayForInventory.Length == 5)
        {
            SaveData.current.inventory = arrayForInventory;
            SerializationManager.Save("0", SaveData.current);
            SaveManager.current.GameSaveInvoke();
        }
        else
            Debug.LogError("this cannot be done");
    }
}
