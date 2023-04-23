using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HideAndShowInteraction : MonoBehaviour
{
    public float maxRange = 3;

    public GameObject child;

    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // singlize a value and also stops strange range bug.
        maxRange = player.GetComponent<ResponsiveSelector>().maxDistance;


        if (child == null)
        {
            child = transform.GetChild(0).gameObject;

            if (child == gameObject)
            {
                throw new NullReferenceException("The object was the same as the parent");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(transform.position, player.position));

        if (Vector3.Distance(transform.position, player.position) <= 4)
        {
            if (!child.activeSelf) child.SetActive(true);
        }
        else
        {
            if (child.activeSelf) child.SetActive(false);
        }
    }
}
