using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunItemInfo")]
public class GunInfo : ItemInfo
{
    public float damage;
    public float fireRate;
    public float reloadSpeed;
    public int maxAmmoInClip;
    public int maxAmmoInReserve;
}
