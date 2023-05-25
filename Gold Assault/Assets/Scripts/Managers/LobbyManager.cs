using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private PauseMenu pm;

    public GameObject LevelSelectorUI;
    public GameObject LoadoutUI;

    public GameObject EquipButton;

    public GameObject ItemInformationSection;

    public GameObject[] WeaponSections;

    public GameObject[] ItemSlots;

    public ItemInformationForLoadout[] itemInformations = new ItemInformationForLoadout[5];

    // loadout
    private int[] tempInventory = new int[5];

    private int selectedItemSlotType = 0;
    private int selectedItemID = 0;

    private bool LevelSelectorIsOpen = false;
    private bool LoadoutIsOpen = false;


    //map information
    public GameObject mapDescriptionObject;

    public TMP_Text MapName;
    public Image MapImage;
    public TMP_Text MapDescription;

    private string mapBuildName;
    private int mapIndexNumber;


    // things for the item information.
    public TMP_Text ItemName;
    public Image ItemImage;
    public TMP_Text ItemDescription;

    public Animator LockerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        LevelSelectorUI.SetActive(false);
        LoadoutUI.SetActive(false);
        ItemInformationSection.SetActive(false);
        EquipButton.SetActive(false);
        mapDescriptionObject.SetActive(false);

        CloseWeaponSections();

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

        GameObject[] temp_gameObjectArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in temp_gameObjectArray)
        {
            if (go.name == "Player")
            {
                pm = go.GetComponent<PauseMenu>();
            }
        }

    }

    private void OnSaveGame()
    {
        LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSelectorIsOpen || LoadoutIsOpen)
        {
            pm.OveridingEscape = true;

            pm.OveridingTimeScale = true;

            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (LevelSelectorIsOpen)
                {
                    CloseMap();
                }

                if (LoadoutIsOpen)
                {
                    CloseLoadout();
                }
            }
        }
        else
        {
            // this may cause errors - making a future not about it.|A
            pm.OveridingEscape = false;
            pm.OveridingTimeScale = false;
        }
    }

    public void OpenMap()
    {
        LevelSelectorUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LevelSelectorIsOpen = true;
        mapDescriptionObject.SetActive(false);
    }

    public void CloseMap()
    {
        LevelSelectorUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        LevelSelectorIsOpen = false;
    }

    public void OpenMapDescription(MapInformation mapInformation)
    {
        MapName.text = mapInformation.nameOfMap;
        MapImage.sprite = mapInformation.image;
        MapDescription.text = mapInformation.description;

        mapBuildName = mapInformation.mapBuildName;
        mapIndexNumber = mapInformation.mapIndexNumber;

        mapDescriptionObject.SetActive(true);
    }

    public void CloseMapDescription()
    {
        mapDescriptionObject.SetActive(false);

    }

    public void LoadSelectedLevel()
    {

        Debug.LogErrorFormat("Something went worng! name of map {0}, map index {1} and gotten index {2} ", mapBuildName, mapIndexNumber, SceneManager.GetSceneByName(mapBuildName).buildIndex);
        LoadMapName(mapBuildName);
    }

    public void OpenLoadout()
    {
        LoadoutUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LoadoutIsOpen = true;
        LockerAnimator.SetBool("Open", true);
    }

    public void CloseLoadout()
    {
        LoadoutUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        LoadoutIsOpen = false;
        LockerAnimator.SetBool("Open", false);
    }

    public void OpenWeaponSections(int _i)
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

    public void OpenLastWeaponSection(int _i)
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

    public void CloseWeaponSections()
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
        SaveData.current.inventory = tempInventory;
        SaveManager.current.ForceSave();
        ItemInformationSection.SetActive(false);
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

    private void LoadMap(int id)
    {
        if (GameManager.current != null)
            GameManager.current.LoadMap(id);
        else
            SceneManager.LoadSceneAsync(id, LoadSceneMode.Single);
    }

    private void LoadMapName(string name)
    {
        if (GameManager.current != null)
            GameManager.current.LoadMapWithName(name);
        else
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }
}
