using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorMK2 : MonoBehaviour
{
    public GameObject door;
    public GameObject? door2;

    public float timeDelay = 0.2f;
    public float timeDelayOther = 0.2f;

    public float power = 3f;

    private bool isOpen = false;
    private bool isOtherDoorOpen = false;

    private float currentTime = 0;
    private float currentTimeOther = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    //* AI incoperation, the AI cannot see the door, they will attempt to go through them.

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "AI")
        {
            if (!isOpen && !isOtherDoorOpen)
            {
                OpenAndCloseBothDoors(other.transform);
            }
            else if (isOpen && !isOtherDoorOpen)
            {
                OpenAndCloseOtherDoor(other.transform);
            }
            else if (!isOpen && isOtherDoorOpen)
            {
                OpenAndClose(other.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime <= timeDelay)
            currentTime += Time.deltaTime;

        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTime >= timeDelay)
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            door.transform.rotation = Quaternion.identity;
        }

        if (door2 != null)
        {
            if (currentTimeOther <= timeDelayOther)
                currentTimeOther += Time.deltaTime;

            if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTimeOther >= timeDelayOther)
            {
                Rigidbody rb = door2.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                door2.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void OpenAndClose(Transform player)
    {
        DoorOpenClose(player);
    }

    public void OpenAndCloseOtherDoor(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        DoorOpenCloseOther(player);
    }

    private void DoorOpenClose(Transform player)
    {
        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }

        currentTime = 0;

        Vector3 dirWithPlayer = door.transform.position - player.position;
        Vector3 dir = door.transform.position - transform.position;

        dirWithPlayer.Normalize();
        dir.Normalize();

        Rigidbody rb = door.GetComponent<Rigidbody>();

        if (isOpen)
        {
            if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) > 0.7f)
            {
                rb.AddForce(door.transform.forward * (power * 0.8f), ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) < 0.7f)
            {
                rb.AddForce(-door.transform.forward * (power * 0.8f), ForceMode.Impulse);
            }
        }
        else
        {
            rb.AddForce(dirWithPlayer * power, ForceMode.Impulse);
        }
    }

    private void DoorOpenCloseOther(Transform player)
    {
        if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            isOtherDoorOpen = false;
        }
        else
        {
            isOtherDoorOpen = true;
        }

        currentTimeOther = 0;

        Vector3 dirWithPlayer = door2.transform.position - player.position;
        Vector3 dir = door2.transform.position - transform.position;

        dirWithPlayer.Normalize();
        dir.Normalize();

        Rigidbody rb = door2.GetComponent<Rigidbody>();

        if (isOtherDoorOpen)
        {
            if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) > 0.7f)
            {
                rb.AddForce(door2.transform.forward * (power * 0.8f), ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) < 0.7f)
            {
                rb.AddForce(-door2.transform.forward * (power * 0.8f), ForceMode.Impulse);
            }
        }
        else
        {
            rb.AddForce(dirWithPlayer * power, ForceMode.Impulse);
        }
    }

    public void OpenAndCloseBothDoors(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        if (!isOpen && isOtherDoorOpen)
            DoorOpenClose(player);
        else if (isOpen && !isOtherDoorOpen)
            DoorOpenCloseOther(player);
        else
        {
            DoorOpenClose(player);
            DoorOpenCloseOther(player);
        }

    }
}
