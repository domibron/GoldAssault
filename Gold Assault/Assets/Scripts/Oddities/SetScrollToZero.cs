using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScrollToZero : MonoBehaviour
{
    public float valueToSet = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Scrollbar>().value = valueToSet;
    }
}
