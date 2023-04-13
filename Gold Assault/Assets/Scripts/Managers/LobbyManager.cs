using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public GameObject LevelSelectorUI;
    public GameObject LoadoutUI;

    public GameObject ItemInformationSection;

    public GameObject[] WeaponSections;

    public GameObject[] ItemSlots;

    public ItemInformationForLoadout[] itemInformation = new ItemInformationForLoadout[5];

    private int selectedItemSlotType = 0;
    private int selectedItemID = 0;

    // things for the item information.
    public TMP_Text ItemName;
    public Image ItemImage;
    public TMP_Text ItemDescription;

    // Start is called before the first frame update
    void Start()
    {
        LevelSelectorUI.SetActive(false);
        LoadoutUI.SetActive(false);
        ItemInformationSection.SetActive(false);

        closeWeaponSections();

        if (MasterManger.current == null)
        {
            // retry or create item.
            // then refrence to the item
            // dont need to reference it i belive.

            MGObackupboot.CheckAndCreateMasterManager();
        }

        if (SaveManager.current == null)
        {
            SMBackupBoot.CheckAndCreateSaveManager();
        }

        // * load inventory into the temp inv so i can use it later. // hmmm

        SaveManager.current.onSave += OnSaveGame;

        LoadInventory();

    }

    private void OnSaveGame()
    {
        SaveManager.current.getCurrentInventory();
        LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMap()
    {
        LevelSelectorUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseMap()
    {
        LevelSelectorUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenLoadout()
    {
        LoadoutUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseLoadout()
    {
        LoadoutUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openWeaponSections(int _i)
    {
        for (int i = 0; i < WeaponSections.Length; i++)
        {
            if (i == _i)
            {
                WeaponSections[i].SetActive(true);
                ItemSelected(itemInformation[i]);
            }
            else
            {
                WeaponSections[i].SetActive(false);
            }
        }
    }

    public void closeWeaponSections()
    {
        for (int i = 0; i < WeaponSections.Length; i++)
        {
            WeaponSections[i].SetActive(false);
        }
    }

    public void ItemSelected(ItemInformationForLoadout itemInformation) // SO
    {
        ItemName.text = itemInformation.nameOfItem;
        ItemImage.sprite = itemInformation.image;
        ItemDescription.text = itemInformation.description;

        selectedItemSlotType = itemInformation.ItemSlotType;
        selectedItemID = itemInformation.IndexOfItem;

        ItemInformationSection.SetActive(true);
    }

    public void EquipItem(int slotType, int index)
    {

    }

    private void LoadInventory()
    {
        // save data
        // no save data?
        // then create basic.

        if (SaveManager.current == null)
        {
            Debug.LogError("There was no Save Manager");
            return;
        }

        int[] _tempInv = SaveManager.current.getCurrentInventory();

        for (int i = 0; i < _tempInv.Length; i++)
        {
            //ItemInformationForLoadout IIFL;

            try
            {
                //IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/" + plusOne + "/" + _tempInv[i]);
                SetupItem(i, _tempInv[i]);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarningFormat("An error occurred at index {0} at slot {1}", _tempInv[i], i);
                SetupItem(i);
                //IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/0");
            }
            catch (Exception e)
            {
                Debug.LogError("cannot compleate loading of inventory" + e.GetType());
            }

            //print(IIFL.name);

            //ItemSlots[i].GetComponentInChildren<Image>().sprite = IIFL.image;

            // player equip.


        }
    }

    private void SetupItem(int slot, int id)
    {
        try
        {
            ItemInformationForLoadout IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/" + (slot + 1) + "/" + id);
            ItemSlots[slot].GetComponentInChildren<Image>().sprite = IIFL.image;
            itemInformation[slot] = IIFL;

        }
        catch
        {
            throw new NullReferenceException();
        }
    }

    private void SetupItem(int slot)
    {
        ItemInformationForLoadout IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/0");
        ItemSlots[slot].GetComponentInChildren<Image>().sprite = IIFL.image;
        itemInformation[slot] = IIFL;
    }
}
