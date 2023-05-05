using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitBoxDamage : MonoBehaviour, IDamagable
{
    public int bodyPart;

    [SerializeField] private AdvanceHealthSystemAI advanceHealthSystem;

    // Start is called before the first frame update
    void Start()
    {
        //advanceHealthSystem = GetComponentInParent<AdvanceHealthSystemAI>();
    }

    void IDamagable.TakeDamage(float damage)
    {
        advanceHealthSystem.TakeDamageWhere(bodyPart, damage);
    }
}
