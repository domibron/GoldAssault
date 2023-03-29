using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);
        Debug.DrawRay(transform.position, transform.up, Color.blue, 1f);
        Debug.DrawRay(transform.position, transform.right, Color.yellow, 1f);
    }
}
