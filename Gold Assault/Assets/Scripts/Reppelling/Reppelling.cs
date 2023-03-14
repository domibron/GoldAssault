using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reppelling : MonoBehaviour
{
    [SerializeField] Vector2 minAreas = new Vector2(-1, -1);
    [SerializeField] Vector2 maxAreas = new Vector2(1, 1);

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3((transform.position.y + maxAreas.y + transform.position.y + minAreas.y) / 2, (transform.position.x + maxAreas.x + transform.position.x + minAreas.x) / 2, 1), new Vector3(maxAreas.x, maxAreas.y, 1));
        Gizmos.DrawSphere(transform.position + Vector3.up * maxAreas.y, 0.5f);
        Gizmos.DrawSphere(transform.position + Vector3.right * maxAreas.x, 0.5f);
        Gizmos.DrawSphere(transform.position + Vector3.up * minAreas.y, 0.5f);
        Gizmos.DrawSphere(transform.position + Vector3.right * minAreas.x, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
