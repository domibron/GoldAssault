using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamage : MonoBehaviour, IDamagable
{
    public int bodyPart;

    private HealthSystem hs;

    // Start is called before the first frame update
    void Start()
    {
        hs = GetComponentInParent<HealthSystem>();
    }

    void IDamagable.TakeDamage(float damage)
    {
        hs.TakeDamageWhere(bodyPart, damage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
