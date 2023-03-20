using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void UseMouse0();
    public abstract override void UseRKey();
    public abstract override void IndexEquip(int index);

    //public GameObject bulletImpactPrefab;
}
