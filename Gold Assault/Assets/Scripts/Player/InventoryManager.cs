using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public GameObject _Master;

    public GameObject[] inventory = new GameObject[5];

    [Header("OVERRIDE THE INVENTORY"), Space(1f)]
    public bool overrideInventory = false;
    public int[] overrideInventoryIndex = { 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        // * singletons
        // _Master = get the thingy that allows to only have one 

        SaveManager.current.onSave += OnSaveGame;

        // try
        // {


        // }
        // catch
        // {


        // }


        try // this is to stop the game from crashing / complaining if the Master was not loaded. it will still load base weapons.
        {
            if (!overrideInventory) // get rid of the if statement on release, because people attually want to use their weapons. might no, as its usful.
            {
                SetUpInventory(SaveData.current.inventory);

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
        // todo finish this section of the inventory

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipItem(4);
        }

    }

    private void OnSaveGame()
    {
        foreach (GameObject go in inventory)
        {
            Destroy(go);
        }

        try // this is to stop the game from crashing / complaining if the Master was not loaded. it will still load base weapons.
        {
            if (!overrideInventory) // get rid of the if statement on release, because people attually want to use their weapons. might no, as its usful.
            {
                SetUpInventory(SaveData.current.inventory);

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

    private void EquipItem(int i)
    {
        for (int x = 0; x < inventory.Length; x++)
        {
            if (x == i)
                Equip(x);
            else
                DeEquip(x);
        }
    }

    private void Equip(int i)
    {
        inventory[i].SetActive(true);
        // todo put more magic here.
    }

    private void DeEquip(int i)
    {
        inventory[i].SetActive(false);
    }

    private void SetUpInventory(int[] itemID)
    {
        // i + 1 because 0 does not exist
        // this means we can use the length and do not need to - 1 from it.
        for (int i = 0; i < itemID.Length; i++)
        {
            try
            {
                CreateItem(i + 1, itemID[i]);
            }
            catch (NullReferenceException e)
            {
                Debug.LogWarning($"NRE! - {e.Message} at {e.TargetSite} \n" +
                $"Error trying to Instantiate. Index: {i + 1} weapon ID: {itemID[i]}");
                CreateItem(i + 1); // this still creates the 0 object.
            }
            catch
            {
                Debug.LogError("Critical Error! There was a error trying to set up inventory.");
            }
        }

        EquipItem(0);
    }

    private void CreateItem(string indexNumber, string weaponID)
    {
        try
        {
            PlayerController PC = GetComponentInParent<PlayerController>();
            GameObject item = Instantiate(Resources.Load("Items/" + Path.Combine(indexNumber, weaponID), typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[int.Parse(indexNumber)] = item;
            inventory[int.Parse(indexNumber) - 1] = item;
            item.transform.localPosition = Vector3.zero; // replace with the target position
            item.transform.localRotation = Quaternion.identity;
            item.name = $"ID: {weaponID} Slot: {indexNumber}";
            PC.L_inventory[int.Parse(indexNumber) - 1] = item;

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
            PlayerController PC = GetComponentInParent<PlayerController>();
            GameObject item = Instantiate(Resources.Load("Items/" + Path.Combine(indexNumber.ToString(), weaponID.ToString()), typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[indexNumber] = item;
            inventory[indexNumber - 1] = item;
            item.transform.localPosition = Vector3.zero; // replace with the target position
            item.transform.localRotation = Quaternion.identity;
            item.name = $"ID: {weaponID} Slot: {indexNumber}";
            PC.L_inventory[indexNumber - 1] = item;
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
            PlayerController PC = GetComponentInParent<PlayerController>();
            GameObject item = Instantiate(Resources.Load("Items/0", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity, transform.Find("Slot" + indexNumber));
            // inventory[indexNumber] = item;
            inventory[indexNumber - 1] = item;
            item.transform.localPosition = Vector3.zero; // replace with the target position
            item.transform.localRotation = Quaternion.identity;
            item.name = $"ID: DebugItem Slot: {indexNumber}";
            PC.L_inventory[indexNumber - 1] = item;
        }
        catch
        {
            throw new NullReferenceException();
        }
    }
}
