using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    public TMP_Text TextBox;
    public float waitTime = 0.5f;

    string str;
    string str2 = "<br>Please Wait";

    int stage = 0;

    float time;
    float timeToWait;


    // Start is called before the first frame update
    void Start()
    {
        str = "Loading";
        timeToWait = time + waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= timeToWait)
        {
            if (stage == 3)
                stage = 0;
            else
                stage++;

            switch (stage)
            {
                case 0:
                    str = "Loading";
                    break;
                case 1:
                    str = "Loading.";
                    break;
                case 2:
                    str = "Loading..";
                    break;
                case 3:
                    str = "Loading...";
                    break;
            }

            timeToWait = time + waitTime;
        }


        time += Time.deltaTime;

        TextBox.text = str + str2;
    }
}
