using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveSelector : MonoBehaviour, ISelector
{
    [SerializeField] private List<GameObject> selectables;
    [SerializeField] private float threshold = 0.9f;
    [SerializeField] public float maxDistance = 5f;

    private Transform _selection;

    public void Awake()
    {
        GameObject[] temp__ = GameObject.FindGameObjectsWithTag("Selectable");

        if (temp__.Length <= 0) return;

        for (int i = 0; i < temp__.Length; i++)
        {
            if (temp__[i].GetComponent<ItemInteract>() == null) continue;

            selectables.Add(temp__[i]);
        }

    }

    void ISelector.Check(Ray ray)
    {
        _selection = null;

        var closest = 0f;
        var distance = float.MaxValue;

        for (int i = 0; i < selectables.Count; i++)
        {
            if (selectables[i] == null) selectables.RemoveAt(i);

            var vector1 = ray.direction;
            var vector2 = selectables[i].transform.position - ray.origin;

            var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            var distanceCalc = Vector3.Distance(selectables[i].transform.position, transform.position);

            // selectables[i].lookPercentage = lookPercentage;

            if (lookPercentage > threshold && lookPercentage > closest && distanceCalc < distance && distanceCalc < maxDistance)
            {
                closest = lookPercentage;
                distance = distanceCalc;
                _selection = selectables[i].transform;
            }

        }

    }

    Transform ISelector.GetSelection()
    {
        return _selection;
    }
}
