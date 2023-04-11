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

    private int[] TempInv = { 1, 1, 0, 0, 0 };

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

        if (MasterManger.Instance == null)
        {
            // retry or create item.
            // then refrence to the item
        }

        // * load inventory into the temp inv so i can use it later.

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
        ItemInformationForLoadout IIFL = null;

        try
        {
            IIFL = (ItemInformationForLoadout)Resources.Load("ItemInformation/" + "2" + "/" + "1");
        }
        catch
        {
            Debug.LogWarning("An error occurred!");
        }


        ItemSlots[1].GetComponentInChildren<Image>().sprite = IIFL.image;

    }
}
