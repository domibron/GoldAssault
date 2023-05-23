using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableScript : Throwable
{


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
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Throw");

        }
        else if (!Input.GetMouseButton(1) && !Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Ready", false);
        }
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
