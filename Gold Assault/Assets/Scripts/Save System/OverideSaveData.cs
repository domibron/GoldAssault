using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverideSaveData : MonoBehaviour
{
    [ContextMenuItem("UPDATE INVENTORY SAVE", "inventorModi")]
    public int[] inven = new int[5];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void inventorModi()
    {
        ModifyInventory(inven);
    }

    public IEnumerator waitTilLoaded()
    {
        if (!SerializationManager.Save("0", SaveData.current))
        {
            print("n0o");
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            print("yes");
            SaveManager.current.GameSave();
        }
    }

    public void ModifyInventory(int[] arrayForInventory)
    {
        if (arrayForInventory.Length == 5)
        {
            SaveData.current.inventory = arrayForInventory;
            SerializationManager.Save("0", SaveData.current);
            StartCoroutine(waitTilLoaded());
        }
        else
            Debug.LogError("this cannot be done");
    }
}
