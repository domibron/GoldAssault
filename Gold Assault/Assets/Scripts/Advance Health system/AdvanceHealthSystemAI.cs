using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceHealthSystemAI : MonoBehaviour
{
    public float maxHealth = 100f;

    public float bloodLevel = 100f;

    public float helmet = 100f;
    public float bodyArmor = 100f;

    private float stageOneHealth = 80f;
    private float stageTwoHealth = 50f;
    private float stageThreeHealth = 20f;


    //[HideInInspector]
    public float[] playerBody = { 100f, 100f, 100f, 100f, 100f, 100f };
    // 0 = head
    // 1 = body
    // 2 = left arm
    // 3 = left leg
    // 4 = right arm
    // 5 = right leg

    private bool bleeding = false;
    private float bleedMultipliyer = 0f;

    public ParticleSystem particleSystemBleed;

    void Awake()
    {
        // ? instance?

        for (int i = 0; i < playerBody.Length; i++)
        {
            playerBody[i] = maxHealth;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerBody[0] <= 0)
        {
            // player is dead
            Destroy(this.gameObject);

        }

        if (playerBody[1] <= 0)
        {
            // player is dead
            Destroy(this.gameObject);
        }

        if (bloodLevel <= 0)
        {
            // player is dead
            Destroy(this.gameObject);
        }

        if (bleeding)
        {
            // AI cannot stop bleeding by using a key, they need to think for them selfs sometimes.
            // I say, let the bleed to death if they cannot fight to survive.

            bloodLevel -= Time.deltaTime * 3f * bleedMultipliyer;

            if (!particleSystemBleed.isPlaying)
                particleSystemBleed.Play();

        }
    }

    public void TakeDamageWhere(int bodyPart, float damage)
    {
        if (bodyPart == 0 && helmet > 0) // if the player got hit in the head and the helmet is ok then deal damage to the helmet.
        {
            helmet -= damage;
        }
        else if (bodyPart == 0 && helmet <= 0)
        {
            playerBody[bodyPart] -= damage * 600f;
        }
        else if (bodyPart == 1 && bodyArmor > 0) // if the player's body armor is ok and the hit is in the body then deal damage into body armor.
        {
            bodyArmor -= damage;
        }
        else if (bodyPart == 1 && bodyArmor <= 0) // if the player's body armor is ok and the hit is in the body then deal damage into body armor.
        {
            playerBody[bodyPart] -= damage * 1f;

            RollBleed();
        }
        else
        {
            playerBody[bodyPart] -= damage * 0.8f;
            // roll to recive bleed damage.

            RollBleed();
        }

    }

    private void RollBleed()
    {
        if (Random.Range(1, 100) < 27f)
        {
            // bleed out;
            bleeding = true;

            bleedMultipliyer += 1;
        }
    }
}
