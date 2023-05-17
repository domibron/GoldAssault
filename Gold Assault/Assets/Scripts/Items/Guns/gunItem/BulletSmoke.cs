using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSmoke : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void CreateLine(Vector3 startPos, Vector3 endPos, float duration)
    {
        Vector3[] _array = { startPos, endPos };
        lineRenderer.SetPositions(_array);
        Destroy(this.gameObject, duration);
    }
}
