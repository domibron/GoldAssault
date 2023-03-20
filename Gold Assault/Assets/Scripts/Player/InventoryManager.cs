using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public GameObject _Master;

    public Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();

    [Header("OVERRIDE THE INVENTORY"), Space(1f)]
    public bool overrideInventory = false;
    public int[] overrideInventoryIndex = { 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        // singletons
        // _Master = get the thingy that allows to only have one 
        try // this is to stop the game from crashing / complaining if the Master was not loaded. it will still load base weapons.
        {
            if (!overrideInventory) // get rid of the if statement on release, because people attually want to use their weapons.
            {
                throw new System.NullReferenceException();

            }
            else if (overrideInventory)
            {
                SetUpInventory(overrideInventoryIndex);

            }
        }
        catch (NullReferenceException)
        {
            print("Failed to load");
            // CreateItem(1, 0);
            int[] zero = { 0, 0, 0, 0, 0 };
            SetUpInventory(zero);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            print("4");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("5");
        }

    }

    private void SetUpInventory(int[] itemID)
    {
        // i + 1 because 0 does not exist
        // this means we can use the length and do not need to - 1 from it.
        for (int i = 0; i <= itemID.Length; i++)
        {
            try
            {
                if (itemID[0] != 0)
                    CreateItem(i + 1, itemID[i]);
                else
                    CreateItem(i);

            }
            catch
            {
                print("Error trying to Instantiate. Index: " + (i + 1) + " weapon ID: " + itemID[i]);
            }
        }
    }

    private void CreateItem(string indexNumber, string weaponID)
    {
        try
        {
            GameObject item = Instantiate(Resources.Load("Items/" + Path.Combine(indexNumber, weaponID), typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[int.Parse(indexNumber)] = item;
            inventory.Add(int.Parse(indexNumber), item);
            item.transform.localPosition = Vector3.zero; // replace with the target position

        }
        catch
        {
            throw new NullReferenceException();
        }

    }

    private void CreateItem(int indexNumber, int weaponID)
    {
        try
        {
            GameObject item = Instantiate(Resources.Load("Items/" + Path.Combine(indexNumber.ToString(), weaponID.ToString()), typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[indexNumber] = item;
            inventory.Add(indexNumber, item);
            item.transform.localPosition = Vector3.zero; // replace with the target position
        }
        catch
        {
            throw new NullReferenceException();
        }
    }

    private void CreateItem(int indexNumber)
    {
        try
        {
            GameObject item = Instantiate(Resources.Load("Items/0", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[indexNumber] = item;
            inventory.Add(indexNumber, item);
            item.transform.localPosition = Vector3.zero; // replace with the target position
        }
        catch
        {
            throw new NullReferenceException();
        }
    }

    private void EquipItem(int Index)
    {
        // if ()
        // set objects to inactive,
        // make object active
    }
}
