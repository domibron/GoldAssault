using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarnSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<TMP_Text>().text = $"<b>DO NOT DISTRIBUTE</b><br>Game Version: {Application.version}"
        + "<br>This is a prototype build and is not meant for the public.<br>Everything is subject to change!";
    }
}
