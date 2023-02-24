using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public GameObject Icon;
    private bool isLooking;
    private bool doorOpen = false;
    private bool noRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        Icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Icon.SetActive(isLooking);
        isLooking = false;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime) // copied this from: https://answers.unity.com/questions/1202034/smooth-90-degree-rotation-on-keypress.html
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        noRotate = false;
    }

    public void lookingAt()
    {
        isLooking = true;
    }

    public void RunInteract()
    { // x Y <-- z
        doorOpen = !doorOpen;
        if (noRotate)
            return;

        if (doorOpen)
        {
            noRotate = true;
            StartCoroutine(RotateMe(Vector3.up * 90, 0.8f));
        }
        else
        {
            noRotate = true;
            StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));
        }


    }
}
