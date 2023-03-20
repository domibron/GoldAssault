using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    GameObject target;
    Rappelling rappelling;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.GetChild(0).gameObject;
        rappelling = GetComponent<Rappelling>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController PC = other.GetComponent<PlayerController>();
            PC.atWindow = true;
            PC.targetLandingArea = target;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController PC = other.GetComponent<PlayerController>();
            PC.atWindow = false;
        }
    }
}
