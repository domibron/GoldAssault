using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorMK2 : MonoBehaviour
{
    [Header("Main door")]
    public GameObject door;
    // public GameObject[] doorInteraction = { };
    [Space]
    public GameObject door_OpenCloseInteraction;
    public GameObject door_ForceInteraction;
    public GameObject door_peakInteraction;

    [Header("Other door")]
#nullable enable // this is to stop unity of logging a warning in console.
    // made nullable so i can make sure it can be nullable.
    public GameObject? door2;
    [Space]
    public GameObject? otherDoor_OpenCloseInteraction;
    public GameObject? otherDoor_ForceInteraction;
    public GameObject? otherDoor_peakInteraction;
    [Space]
    public GameObject? bothDoor_OpenCloseInteraction;
    public GameObject? bothDoor_ForceInteraction;
    public GameObject? bothDoor_peakInteraction;
#nullable restore

    [Header("Door properties")]
    public float timeDelay = 0.2f;
    public float timeDelayOther = 0.2f;

    public float power = 3f;
    public float forcePower = 8f;

    private float disableTime = 1f;
    private float global_currentDisableTime = 0;
    private float door_currentDisableTime = 0;
    private float otherDoor_currentDisableTime = 0;

    private bool isOpen = false;
    private bool isOtherDoorOpen = false;

    private float currentTime = 0;
    private float currentTimeOther = 0;

    // Start is called before the first frame update
    void Start()
    {
        // * important, halfway quant is 0.7071068.
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
        if (global_currentDisableTime <= disableTime) global_currentDisableTime += Time.deltaTime;

        if (door_currentDisableTime <= disableTime) door_currentDisableTime += Time.deltaTime;

        if (otherDoor_currentDisableTime <= disableTime) otherDoor_currentDisableTime += Time.deltaTime;


        if (currentTime <= timeDelay) currentTime += Time.deltaTime;

        DoorStateCheck();
        if (door2 != null) OtherDoorStateCheck();


        HandleVisuals();
    }

    private void DoorStateCheck()
    {
        // FYI this is for the door being closed, aka door states
        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTime >= timeDelay)
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            door.transform.localRotation = Quaternion.identity;
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
    }

    private void OtherDoorStateCheck()
    {
        if (currentTimeOther <= timeDelayOther)
            currentTimeOther += Time.deltaTime;

        // FYI this is for the door being closed, aka door states
        if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f && currentTimeOther >= timeDelayOther)
        {
            Rigidbody rb = door2.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            door2.transform.localRotation = Quaternion.identity;
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

    private void HandleVisuals()
    {
        if (door_currentDisableTime < disableTime || global_currentDisableTime < disableTime)
        {
            if (door_OpenCloseInteraction.activeSelf) door_OpenCloseInteraction.SetActive(false);
            if (door_ForceInteraction.activeSelf) door_ForceInteraction.SetActive(false);
            if (door_peakInteraction.activeSelf) door_peakInteraction.SetActive(false);
        }
        else
        {
            if (!isOpen) // if the door is not open.
            {
                if (!door_peakInteraction.activeSelf) door_peakInteraction.SetActive(true);

                if (!door_ForceInteraction.activeSelf) door_ForceInteraction.SetActive(true);
            }
            else
            {
                if (door_peakInteraction.activeSelf) door_peakInteraction.SetActive(false);

                if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.9f)
                {
                    if (!door_ForceInteraction.activeSelf) door_ForceInteraction.SetActive(true);
                }
                else
                {
                    if (door_ForceInteraction.activeSelf) door_ForceInteraction.SetActive(false);
                }
            }

            if (!door_OpenCloseInteraction.activeSelf) door_OpenCloseInteraction.SetActive(true);
        }

        if (door2 == null) return; // ===============================================================================

        // ! logic for when either of the double door is open only peak one or the other
        // ! also show and hide as well
        // * global ========================================
        if (global_currentDisableTime < disableTime)
        {

            if (bothDoor_OpenCloseInteraction.activeSelf) bothDoor_OpenCloseInteraction.SetActive(false);
            if (bothDoor_ForceInteraction.activeSelf) bothDoor_ForceInteraction.SetActive(false);
            if (bothDoor_peakInteraction.activeSelf) bothDoor_peakInteraction.SetActive(false);
        }
        else
        {
            if ((isOpen && !isOtherDoorOpen) || (isOpen && isOtherDoorOpen) || (!isOpen && isOtherDoorOpen))
            {
                if (bothDoor_OpenCloseInteraction.activeSelf) bothDoor_OpenCloseInteraction.SetActive(false);
                if (bothDoor_ForceInteraction.activeSelf) bothDoor_ForceInteraction.SetActive(false);
                if (bothDoor_peakInteraction.activeSelf) bothDoor_peakInteraction.SetActive(false);
            }
            else
            {
                if (!bothDoor_OpenCloseInteraction.activeSelf) bothDoor_OpenCloseInteraction.SetActive(true);
                if (!bothDoor_ForceInteraction.activeSelf) bothDoor_ForceInteraction.SetActive(true);
                if (!bothDoor_peakInteraction.activeSelf) bothDoor_peakInteraction.SetActive(true);
            }
        }

        // * other door -=-=-==-=-=--==--=-=-=-=-==-=-=-=--=-==--==--=-=-=-==-=-=--=-=-=-==--=-==--=-=-==--==--=-=-==-=---==-

        if (otherDoor_currentDisableTime < disableTime || global_currentDisableTime < disableTime)
        {
            if (otherDoor_OpenCloseInteraction.activeSelf) otherDoor_OpenCloseInteraction.SetActive(false);
            if (otherDoor_ForceInteraction.activeSelf) otherDoor_ForceInteraction.SetActive(false);
            if (otherDoor_peakInteraction.activeSelf) otherDoor_peakInteraction.SetActive(false);
        }
        else
        {
            if (!isOtherDoorOpen) // if the door is not open.
            {
                if (!otherDoor_peakInteraction.activeSelf) otherDoor_peakInteraction.SetActive(true);

                if (!otherDoor_ForceInteraction.activeSelf) otherDoor_ForceInteraction.SetActive(true);
            }
            else
            {
                if (otherDoor_peakInteraction.activeSelf) otherDoor_peakInteraction.SetActive(false);

                if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.9f)
                {
                    if (!otherDoor_ForceInteraction.activeSelf) otherDoor_ForceInteraction.SetActive(true);
                }
                else
                {
                    if (otherDoor_ForceInteraction.activeSelf) otherDoor_ForceInteraction.SetActive(false);
                }
            }

            if (!otherDoor_OpenCloseInteraction.activeSelf) otherDoor_OpenCloseInteraction.SetActive(true);
        }
    }

    public void OpenAndClose(Transform player)
    {
        DoorOpenClose(player);
    }

    public void PeakDoor(Transform player)
    {
        StartCoroutine(PeakDoorRun(player));
    }

    private IEnumerator PeakDoorRun(Transform player)
    {
        door.GetComponent<Rigidbody>().drag = 10;
        DoorOpenClose(player);
        yield return new WaitForSecondsRealtime(1f);
        door.GetComponent<Rigidbody>().drag = 1;
    }

    public void ForceDoor(Transform player)
    {
        ForceDoorOpen(player);
    }

    private void ForceDoorOpen(Transform player)
    {

        if (global_currentDisableTime < disableTime || door_currentDisableTime < disableTime) return;

        door_currentDisableTime = 0;

        currentTime = 0;
        Rigidbody rb = door.GetComponent<Rigidbody>();

        if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            if (Vector3.Dot(transform.forward, player.position - transform.position) > 0)
            {
                rb.AddForce(-door.transform.forward * forcePower, ForceMode.Impulse);
            }
            else if (Vector3.Dot(transform.forward, player.position - transform.position) < 0)
            {
                rb.AddForce(door.transform.forward * forcePower, ForceMode.Impulse);
            }
        }
        else
        {
            if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) > 0.7071068f)
            {
                rb.AddForce(-door.transform.forward * forcePower, ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) < 0.7071068f)
            {
                rb.AddForce(door.transform.forward * forcePower, ForceMode.Impulse);
            }

        }
    }

    private void DoorOpenClose(Transform player)
    {

        if (global_currentDisableTime < disableTime || door_currentDisableTime < disableTime) return;

        door_currentDisableTime = 0;

        currentTime = 0;

        Rigidbody rb = door.GetComponent<Rigidbody>();

        if (isOpen) //  if the door is open
        {
            float local_powerNerf = 1 - Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 0, 0));
            float local_power = (power * local_powerNerf) + 1;

            // if the door is open towards a specific direction
            if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) > 0.7071068f)
            {
                rb.AddForce(door.transform.forward * local_power, ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door.transform.localRotation, Quaternion.Euler(0, 90, 0)) < 0.7071068f)
            {
                rb.AddForce(-door.transform.forward * local_power, ForceMode.Impulse);
            }
        }
        else // used to open the door
        {
            if (Vector3.Dot(transform.forward, player.position - transform.position) > 0)
            {
                rb.AddForce(-door.transform.forward * power, ForceMode.Impulse);
            }
            else if (Vector3.Dot(transform.forward, player.position - transform.position) < 0)
            {
                rb.AddForce(door.transform.forward * power, ForceMode.Impulse);
            }
        }
    }

    // TODO applie changes from door to other door.

    #region Other door

    public void OpenCloseOther(Transform player)
    {
        DoorOtherOpenClose(player);
    }

    public void PeakOtherDoor(Transform player)
    {
        StartCoroutine(PeakOtherDoorRun(player));
    }

    private IEnumerator PeakOtherDoorRun(Transform player)
    {
        door2.GetComponent<Rigidbody>().drag = 10;
        DoorOtherOpenClose(player);
        yield return new WaitForSecondsRealtime(1f);
        door2.GetComponent<Rigidbody>().drag = 1;
    }

    public void ForceOtherDoor(Transform player)
    {
        ForceOtherDoorOpen(player);
    }

    private void ForceOtherDoorOpen(Transform player)
    {

        if (global_currentDisableTime < disableTime || otherDoor_currentDisableTime < disableTime) return;

        otherDoor_currentDisableTime = 0;

        currentTime = 0;

        Rigidbody rb = door2.GetComponent<Rigidbody>();

        if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0)) >= 0.995f)
        {
            if (Vector3.Dot(transform.forward, player.position - transform.position) > 0)
            {
                rb.AddForce(-door2.transform.forward * forcePower, ForceMode.Impulse);
            }
            else if (Vector3.Dot(transform.forward, player.position - transform.position) < 0)
            {
                rb.AddForce(door2.transform.forward * forcePower, ForceMode.Impulse);
            }
        }
        else
        {
            if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) > 0.7071068f)
            {
                rb.AddForce(-door2.transform.forward * forcePower, ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) < 0.7071068f)
            {
                rb.AddForce(door2.transform.forward * forcePower, ForceMode.Impulse);
            }

        }
    }

    private void DoorOtherOpenClose(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        if (global_currentDisableTime < disableTime || otherDoor_currentDisableTime < disableTime) return;

        otherDoor_currentDisableTime = 0;

        currentTimeOther = 0;

        Rigidbody rb = door2.GetComponent<Rigidbody>();

        if (isOtherDoorOpen) //  if the door is open
        {
            float local_powerNerf = 1 - Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, 0, 0));
            float local_power = (power * local_powerNerf) + 1;

            // if the door is open towards a specific direction
            if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) > 0.7071068f)
            {
                rb.AddForce(door2.transform.forward * local_power, ForceMode.Impulse);
            }
            else if (Quaternion.Dot(door2.transform.localRotation, Quaternion.Euler(0, -90, 0)) < 0.7071068f)
            {
                rb.AddForce(-door2.transform.forward * local_power, ForceMode.Impulse);
            }
        }
        else // used to open the door
        {
            if (Vector3.Dot(transform.forward, player.position - transform.position) > 0)
            {
                rb.AddForce(-door2.transform.forward * power, ForceMode.Impulse);
            }
            else if (Vector3.Dot(transform.forward, player.position - transform.position) < 0)
            {
                rb.AddForce(door2.transform.forward * power, ForceMode.Impulse);
            }
        }
    }



    #endregion

    // * both doors =========================================================================

    private void OpenCloseBoth(Transform player)
    {
        OpenAndClose(player);
        OpenCloseOther(player);
        global_currentDisableTime = 0;
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
            OpenCloseBoth(player);
        }

    }

    private void ForceDoors(Transform player)
    {
        ForceDoor(player);
        ForceOtherDoor(player);
        global_currentDisableTime = 0;
    }

    public void ForceBothDoors(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        // ! make into the visual the hiding the force both doors.
        if (!isOpen && !isOtherDoorOpen)
        {
            ForceDoors(player);
        }
    }

    private void PeakDoors(Transform player)
    {
        PeakDoor(player);
        PeakOtherDoor(player);
        global_currentDisableTime = 0;
    }

    public void PeakBothDoors(Transform player)
    {
        if (door2 == null)
        {
            throw new NullReferenceException();
        }

        if (!isOpen && !isOtherDoorOpen)
        {
            PeakDoors(player);
        }
    }
}
