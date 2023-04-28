using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.transform.tag == "Player")
        {

            PlayerController pc = other.GetComponent<PlayerController>();
            pc.currentHealth = pc.maxHealth;

        }
    }
}
