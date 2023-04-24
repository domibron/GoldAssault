using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ItemInforForInv")]
public class ItemInformationForLoadout : ScriptableObject
{
    public string nameOfItem;
    [TextArea] public string description;
    public Sprite image;

    public int ItemSlotType;
    public int IndexOfItem;

    // more if needed.
}
