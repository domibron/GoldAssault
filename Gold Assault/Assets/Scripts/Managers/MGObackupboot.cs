using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MGObackupboot : MonoBehaviour
{
    /*<summery>
	This script will create the master manager if there isnt one.
	*/

    void Awake()
    {
        if (MasterManger.Instance == null)
        {
            Debug.LogWarning("WARNING!\nMaster Manager was not detected, automatically instaciated the object");
            Instantiate(Resources.Load("MasterGameObject", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity);//, transform.Find("Slot" + indexNumber));
        }
    }
}
