using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static float GetPointBetweenTwoNumbers(float max, float min)
    {
        return (max - ((max - min) / 2));
    }
}
