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
        if (GUILayout.Button("Force inventory")) myScript.saveArrayToInventory();

        if (GUILayout.Button("Force sensitivty")) myScript.saveNewSens();
    }
}
// ! ===============================
#endif

public class OverideSaveData : MonoBehaviour
{
    [SerializeField] public int[] _temp = new int[5];

    [SerializeField] public float sensitivity = 1f;

    void Start()
    {
        _temp = SaveData.current.inventory;

        sensitivity = SaveData.current.sensitivity;

        SaveManager.current.onSave += OnSaveGame;
    }

    public void OnSaveGame()
    {
        print("loaded save inv");

        _temp = SaveData.current.inventory;

        sensitivity = SaveData.current.sensitivity;

    }

    public void saveNewSens()
    {
        SaveData.current.sensitivity = sensitivity;
        SerializationManager.Save("0", SaveData.current);
        SaveManager.current.GameSaveInvoke();
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
