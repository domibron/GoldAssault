using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private PlayerController pc;

    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(true);
        print("T true");
    }

    void OnTriggerStay(Collider other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(true);
        print("T true");
    }

    void OnTriggerExit(Collider other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(false);
        print("T false");
    }

    void OnCollisionEnter(Collision other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(true);
        print("C true");
    }

    void OnCollisionStay(Collision other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(true);
        print("C true");
    }

    void OnCollisionExit(Collision other)
    {
        print(other.transform.name);
        if (other.gameObject == pc.gameObject)
            return;

        pc.SetGrounded(false);
        print("C false");
    }
}
