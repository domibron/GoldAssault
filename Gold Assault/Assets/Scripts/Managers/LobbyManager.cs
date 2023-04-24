using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject LevelSelectorUI;
    public GameObject LoadoutUI;

    public GameObject EquipButton;

    public GameObject ItemInformationSection;

    public GameObject[] WeaponSections;

    public GameObject[] ItemSlots;

    public ItemInformationForLoadout[] itemInformations = new ItemInformationForLoadout[5];

    private int[] tempInventory = new int[5];

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
        EquipButton.SetActive(false);

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
                ItemSelectedDisableButton(itemInformations[i]);
            }
            else
            {
                WeaponSections[i].SetActive(false);
            }
        }
    }

    public void openLastWeaponSection(int _i)
    {
        for (int i = 0; i < WeaponSections.Length; i++)
        {
            if (i == _i - 1)
            {
                WeaponSections[i].SetActive(true);
                ItemSelectedDisableButton(itemInformations[_i]);
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

        if (itemInformation != itemInformations[selectedItemSlotType - 1])
            EquipButton.SetActive(true);
        else
            EquipButton.SetActive(false);

        ItemInformationSection.SetActive(true);
    }

    public void ItemSelectedDisableButton(ItemInformationForLoadout itemInformation)
    {
        ItemName.text = itemInformation.nameOfItem;
        ItemImage.sprite = itemInformation.image;
        ItemDescription.text = itemInformation.description;

        selectedItemSlotType = itemInformation.ItemSlotType;
        selectedItemID = itemInformation.IndexOfItem;

        EquipButton.SetActive(false);

        ItemInformationSection.SetActive(true);
    }

    public void EquipItem()
    {
        tempInventory[selectedItemSlotType - 1] = selectedItemID;
    }

    public void EquipAllItems()
    {
        // tempInventory[selectedItemSlotType] = selectedItemID;
        SaveData.current.inventory = tempInventory;
        SaveManager.current.ForceSave();
    }

    private void LoadInventory()
    {
        if (SaveManager.current == null)
        {
            Debug.LogError("There was no Save Manager");
            return;
        }

        int[] _tempInv = SaveData.current.inventory;
        tempInventory = SaveData.current.inventory;

        for (int i = 0; i < _tempInv.Length; i++)
        {
            try
            {
                SetupItem(i, _tempInv[i]);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarningFormat("An error occurred at index {0} at slot {1}", _tempInv[i], i);
                SetupItem(i);
            }
            catch (Exception e)
            {
                Debug.LogError("cannot compleate loading of inventory" + e.GetType());
            }


        }
    }

    private void SetupItem(int slot, int id)
    {
        try
        {
            ItemInformationForLoadout IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/" + (slot + 1) + "/" + id);
            ItemSlots[slot].GetComponentInChildren<Image>().sprite = IIFL.image;
            itemInformations[slot] = IIFL;

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
        itemInformations[slot] = IIFL;
    }

    public void LoadMap(int id)
    {
        if (GameManager.current != null)
            GameManager.current.LoadMap(id);
        else
            SceneManager.LoadSceneAsync(id, LoadSceneMode.Single);
    }
}
