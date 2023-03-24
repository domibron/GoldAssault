using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spin : MonoBehaviour
{
    private RectTransform rt;
    float x = 0;
    float y = 0;
    float z = 0;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        x = 0;
        y = 0;
        z += 1;

        rt.rotation = Quaternion.Euler(x, y, z);
    }
}
