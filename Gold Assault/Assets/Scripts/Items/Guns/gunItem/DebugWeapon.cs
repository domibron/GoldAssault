using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeapon : Gun // index ID is 2 because it is a pistol
{
    private Animator animator;
    bool equipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !equipped)
        {
            equipped = true;
            animator.SetTrigger("Equip");
        }
        else if (!gameObject.activeSelf)
        {
            equipped = false;
        }

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("ADS", true);
        }
        else
        {
            animator.SetBool("ADS", false);
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Fire");
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
