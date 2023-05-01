using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public TMP_Text Text_blood;
    public TMP_Text Text_head;
    public TMP_Text Text_body;
    public TMP_Text Text_leftArm;
    public TMP_Text Text_leftLeg;
    public TMP_Text Text_rightArm;
    public TMP_Text Text_rightLeg;


    public float maxHealth = 100f;

    private float bloodLevel = 100f;

    private float helmet = 100f;
    private float bodyArmor = 100f;

    float[] playerBody = { 100f, 100f, 100f, 100f, 100f, 100f };
    // 0 = head
    // 1 = body
    // 2 = left arm
    // 3 = left leg
    // 4 = right arm
    // 5 = right leg

    PlayerController pc;

    void Awake()
    {
        // ? instance?

        for (int i = 0; i < playerBody.Length; i++)
        {
            playerBody[i] = maxHealth;
        }

        pc = GetComponent<PlayerController>();
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

        }

        if (playerBody[1] <= 0)
        {
            // player is dead
        }



        if (helmet > 0) // this is to show that the player has a helmet
        {
            Text_head.text = $"<color=blue>{playerBody[0] + helmet}</color>";
        }
        else
        {
            Text_head.text = $"<color=black>{playerBody[0]}</color>";
        }

        if (bodyArmor > 0) // this is to show th at the player has body armor
        {
            Text_body.text = $"<color=blue>{playerBody[1] + bodyArmor}</color>";
        }
        else
        {
            Text_body.text = $"<color=black>{playerBody[1]}</color>";
        }

        Text_blood.text = bloodLevel.ToString();
        Text_leftArm.text = playerBody[2].ToString();
        Text_leftLeg.text = playerBody[3].ToString();
        Text_rightArm.text = playerBody[4].ToString();
        Text_rightLeg.text = playerBody[5].ToString();

    }

    public void TakeDamageWhere(int bodyPart, float damage)
    {
        if (bodyPart == 0 && helmet > 0) // if the player got hit in the head and the helmet is ok then deal damage to the helmet.
        {
            helmet -= damage;
        }
        else if (bodyPart == 1 && bodyArmor > 0) // if the player's body armor is ok and the hit is in the body then deal damage into body armor.
        {
            bodyArmor -= damage;
        }
        else
        {
            playerBody[bodyPart] -= damage;
            // roll to recive bleed damage.
        }

    }
}
