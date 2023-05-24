using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlashbang : MonoBehaviour
{
    public float damage = 0f;
    public float stunTime;

    public GameObject flash;

    private float delayTime = 3f;
    private float localTime = 0f;
    private float activationTime = 0f;

    private float range = 5f;

    private Vector3 scale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        scale = flash.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localTime += Time.deltaTime;

        if (localTime >= delayTime)
        {
            Expload();
            activationTime = localTime;
        }

        if (flash.activeSelf)
        {
            flash.transform.localScale = Vector3.Lerp(scale, new Vector3(range, range, range), (localTime - activationTime) * 2f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, range);
    }

    private void Expload()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, Vector3.zero);

        foreach (RaycastHit hit in hits)
        {
            hit.collider.GetComponent<IDamagable>()?.TakeDamage(damage);
            hit.collider.GetComponent<IStunable>()?.GetStunned(stunTime);
        }

        flash.SetActive(true);

        Destroy(this.gameObject, 0.5f);
    }
}
