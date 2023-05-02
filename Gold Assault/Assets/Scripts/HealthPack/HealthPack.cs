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
            HealthSystem hs = other.GetComponent<HealthSystem>();

            for (int i = 0; i < hs.playerBody.Length; i++)
            {
                hs.playerBody[i] = 100f;
            }

            // remove this should never be recoverable.
            hs.bloodLevel = 100f;

        }
    }
}
