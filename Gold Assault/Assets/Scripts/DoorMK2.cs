using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMK2 : MonoBehaviour
{
    public GameObject door;

    public float timeDelay = 0.1f;

    private bool isOpen = false;

    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (!isOpen)
        // {
        // 	print(Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)));


        // }

        if (currentTime <= timeDelay)
            currentTime += Time.deltaTime;

        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTime >= timeDelay)
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            door.transform.rotation = Quaternion.identity;
        }
    }

    public void OpenAndClose(Transform player)
    {
        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

        currentTime = 0;

        Vector3 dirWithPlayer = door.transform.position - player.position;
        Vector3 dir = door.transform.position - transform.position;
        dirWithPlayer.Normalize();
        dir.Normalize();

        if (isOpen)
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.AddForce(-dir * 5f, ForceMode.Impulse);
        }
        else
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.AddForce(dirWithPlayer * 5f, ForceMode.Impulse);
        }
    }
}
