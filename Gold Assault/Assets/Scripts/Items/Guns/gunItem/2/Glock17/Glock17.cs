using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock17 : Gun // index ID is 2 because it is a pistol
{

    private Animator animator;
    bool equipped = false;

    private float localTime = 0f;
    private float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (localTime > 0) localTime -= Time.deltaTime;
        else localTime = 0;

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

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (localTime <= ((GunInfo)itemInfo).fireRate)
        {
            localTime += ((GunInfo)itemInfo).fireRate;
            animator.SetTrigger("Fire");

            // shoot
            Debug.DrawLine(transform.position, transform.forward * 10f, Color.blue, 0.5f);
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
