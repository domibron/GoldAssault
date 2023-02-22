using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IInteractable
{
    Rigidbody rb;

    bool lookedat;
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.SetActive(lookedat);
        lookedat = false;
    }

    void IInteractable.RunInteract()
    {
        rb.AddForce(Vector3.up, ForceMode.Impulse);
    }

    void IInteractable.lookingAt()
    {
        lookedat = true;
    }
}
