using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorMK2 : MonoBehaviour
{
    [Header("Main door")]
    public GameObject door;
    public GameObject[] doorInteracton = { };

    [Header("Other door")]
#nullable enable // this is to stop unity of logging a warning in console.
    public GameObject? door2;
#nullable restore
    public GameObject[] doubleDoorInteraction = { };
    public GameObject[] door2Interaction = { };

    [Header("Door properties")]
    public float timeDelay = 0.2f;
    public float timeDelayOther = 0.2f;

    public float power = 3f;

    private float disableTime = 1f;
    private float currentDisableTime = 0;

    private bool isOpen = false;
    private bool isOtherDoorOpen = false;

    private float currentTime = 0;
    private float currentTimeOther = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    //* AI incoperation, the AI cannot see the door, they will attempt to go through them.

    public void AITriggerDoor(Transform _transform)
    {
        if (door2 != null)
        {
            if (isOpen && isOtherDoorOpen)
            {
                // ...
            }
            else
            {
                OpenAndCloseBothDoors(_transform);
            }
        }
        else
        {
            if (!isOpen)
            {
                OpenAndClose(_transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDisableTime <= disableTime) currentDisableTime += Time.deltaTime;

        if (currentTime <= timeDelay) currentTime += Time.deltaTime;

        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTime >= timeDelay)
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            door.transform.rotation = Quaternion.identity;
            isOpen = false;
        }
        else if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }

        if (door2 != null)
        {
            // hides the floating ,iddle door interaction.
            if ((isOpen && !isOtherDoorOpen) || (isOpen && isOtherDoorOpen) || (!isOpen && isOtherDoorOpen))
            {
                for (int i = 0; i < doubleDoorInteraction.Length; i++)
                {
                    doubleDoorInteraction[i].SetActive(false);
                }

            }
            else
            {
                for (int i = 0; i < doubleDoorInteraction.Length; i++)
                {
                    doubleDoorInteraction[i].SetActive(true);
                }
            }

            if (currentTimeOther <= timeDelayOther)
                currentTimeOther += Time.deltaTime;

            if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTimeOther >= timeDelayOther)
            {
                Rigidbody rb = door2.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                door2.transform.rotation = Quaternion.identity;
                isOtherDoorOpen = false;
            }
            else if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
            {
                isOtherDoorOpen = false;
            }
            else
            {
                isOtherDoorOpen = true;
            }
        }
    }

    public void OpenAndClose(Transform player)
    {
        DoorOpenClose(player);
    }



    private void DoorOpenClose(Transform player)
    {

        if (currentDisableTime < disableTime) return;

        currentDisableTime = 0;

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

    public void OpenCloseOther(Transform player)
    {
        DoorOtherOpenClose(player);
    }

    private void DoorOtherOpenClose(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        if (currentDisableTime < disableTime) return;

        currentDisableTime = 0;


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
        {
            OpenAndClose(player);
        }
        else if (isOpen && !isOtherDoorOpen)
        {
            OpenCloseOther(player);
        }
        else
        {
            OpenAndClose(player);
            OpenCloseOther(player);
        }

    }
}
