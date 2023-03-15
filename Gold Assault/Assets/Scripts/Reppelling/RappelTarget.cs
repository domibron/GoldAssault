using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RappelTarget : MonoBehaviour
{
    private Rappelling rappelling;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        rappelling = GetComponentInParent<Rappelling>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
