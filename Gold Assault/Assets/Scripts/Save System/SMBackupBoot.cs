using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBackupBoot : MonoBehaviour
{
    // void Awake()
    // {
    //     if (SaveManager.Instance == null)
    //     {
    //         Debug.LogWarning("WARNING!\nMaster Manager was not detected, automatically instaciated the object");
    //         Instantiate(Resources.Load("MasterGameObject", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity);//, transform.Find("Slot" + indexNumber));
    //     }
    // }

    public static SaveManager CheckAndCreateSaveManager()
    {
        if (SaveManager.current == null)
        {
            Debug.LogWarning("WARNING!\nMaster Manager was not detected, automatically instaciated the object");
            Instantiate(Resources.Load("SaveManager", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity);//, transform.Find("Slot" + indexNumber));
            return SaveManager.current;
        }
        else
        {
            return SaveManager.current;
        }
    }
}
