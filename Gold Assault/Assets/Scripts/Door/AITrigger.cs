using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrigger : MonoBehaviour
{
    private DoorMK2 doorParent;

    // Start is called before the first frame update
    void Start()
    {
        doorParent = GetComponentInParent<DoorMK2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "AI")
        {
            doorParent.AITriggerDoor(other.transform);
        }
    }
}
