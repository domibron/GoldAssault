using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachingCharge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Expload()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Wall wall = hit.collider.gameObject.GetComponent<Wall>();
                wall.AddBulletHole(ray, 4f);
            }
        }

        Destroy(this.gameObject);

    }
}
