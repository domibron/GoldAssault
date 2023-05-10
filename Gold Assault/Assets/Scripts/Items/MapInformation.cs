using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/MapInformation")]
public class MapInformation : ScriptableObject
{
    public string nameOfMap;
    [TextArea] public string description;
    public Sprite image;

    public int mapIndexNumber;
    public string mapBuildName;
}
