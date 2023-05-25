using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ThrowableInfo")]
public class ThrowableInfo : ItemInfo
{
    public float damage;
    // public float fireRate;
    // public float reloadSpeed;
    public int maxAmmoount;
    // public int maxAmmoMags;
    // public Vector3 startingPos;
    public object throwableObject;
}
