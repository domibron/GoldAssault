using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Selectables : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lookpercentageLable;

    public float lookPercentage;

    // Update is called once per frame
    void Update()
    {
        lookpercentageLable.text = lookPercentage.ToString("F3");
    }
}
