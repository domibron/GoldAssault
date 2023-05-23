using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlashbang : MonoBehaviour
{
    public float damage = 0f;
    public float stunTime;

    private float delayTime = 3f;
    private float localTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        localTime += Time.deltaTime;

        if (localTime >= delayTime)
        {
            Expload();
        }
    }

    private void Expload()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, Vector3.zero);

        foreach (RaycastHit hit in hits)
        {
            hit.collider.GetComponent<IDamagable>()?.TakeDamage(damage);
            hit.collider.GetComponent<IStunable>()?.GetStunned(stunTime);
        }
    }
}
