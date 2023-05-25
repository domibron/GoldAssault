using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableScript : Throwable
{

    public GameObject FlashbangObject;

    public Transform instanciationPoint;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Spare", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Ready", true);
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Throw");
                ThrowObject();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Throw");
            ThrowObject();

        }
        else if (!Input.GetMouseButton(1) && !Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Ready", false);
        }
    }

    private void ThrowObject()
    {
        GameObject item = Instantiate(FlashbangObject, instanciationPoint.position, Quaternion.Euler(0, 0, 0));
        item.transform.LookAt(transform.position);
        item.GetComponent<ObjectFlashbang>().damage = ((ThrowableInfo)itemInfo).damage;

        // TODO ending need changing
        item.GetComponent<ObjectFlashbang>().stunTime = ((ThrowableInfo)itemInfo).damage;

        item.GetComponent<Rigidbody>().AddForce(-(item.transform.forward) * 8f, ForceMode.Impulse);
        item.GetComponent<Rigidbody>().AddForce((item.transform.up) * 8f, ForceMode.Impulse);
    }



















    public override void UseMouse0()
    {
        throw new System.NotImplementedException();
    }

    public override void UseRKey()
    {
        throw new System.NotImplementedException();
    }
}
