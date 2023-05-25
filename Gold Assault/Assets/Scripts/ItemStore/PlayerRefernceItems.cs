using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefernceItems : MonoBehaviour
{
    public static PlayerRefernceItems current;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
        }
    }


    public List<GameObject> AINoiseAlertSubs;
}
