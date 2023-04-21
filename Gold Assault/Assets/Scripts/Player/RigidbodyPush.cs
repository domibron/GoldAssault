using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RigidbodyPush : MonoBehaviour
{
    [SerializeField]
    private float forcePower = 2f;

    public string[] IgnoreTags;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        // print(rb);S

        if (rb != null)
        {
            if (IgnoreTags.All(hit.transform.tag.Contains))
            {
                return;
            }

            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rb.AddForceAtPosition(forceDirection * forcePower, transform.position, ForceMode.Impulse);
        }
    }
}
