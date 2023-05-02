using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public float bloodLevel = 100f;

    public float helmet = 100f;
    public float bodyArmor = 100f;

    [HideInInspector]
    public float[] playerBody = { 100f, 100f, 100f, 100f, 100f, 100f };
    // 0 = head
    // 1 = body
    // 2 = left arm
    // 3 = left leg
    // 4 = right arm
    // 5 = right leg

    PlayerController pc;

    private bool bleeding = false;

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
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);

        }

        if (playerBody[1] <= 0)
        {
            // player is dead
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
        }

        if (bloodLevel <= 0)
        {
            // player is dead
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex);
        }

        if (bleeding)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                bleeding = false;
            }

            bloodLevel -= Time.deltaTime * 4f;


            if (bloodLevel >= 66f)
            {
                Text_blood.text = $"<color=green>{bloodLevel.ToString("F0")}</color> - press F to stop bleeding";
            }
            else if (bloodLevel >= 33f)
            {
                Text_blood.text = $"<color=yellow>{bloodLevel.ToString("F0")}</color> - press F to stop bleeding";
            }
            else
            {
                Text_blood.text = $"<color=red>{bloodLevel.ToString("F0")}</color> - press F to stop bleeding";
            }

        }
        else if (bloodLevel >= 66f)
        {
            Text_blood.text = $"<color=green>{bloodLevel.ToString("F0")}</color>";
        }
        else if (bloodLevel >= 33f)
        {
            Text_blood.text = $"<color=yellow>{bloodLevel.ToString("F0")}</color>";
        }
        else
        {
            Text_blood.text = $"<color=red>{bloodLevel.ToString("F0")}</color>";
        }

        #region UI

        if (helmet > 0) // this is to show that the player has a helmet
        {
            Text_head.text = $"<color=blue>{playerBody[0] + helmet}</color>";
        }
        else if (playerBody[0] >= 66f)
        {
            Text_head.text = $"<color=green>{playerBody[0]}</color>";
        }
        else if (playerBody[0] >= 33f)
        {
            Text_head.text = $"<color=yellow>{playerBody[0]}</color>";
        }
        else
        {
            Text_head.text = $"<color=red>{playerBody[0]}</color>";
        }


        if (bodyArmor > 0) // this is to show th at the player has body armor
        {
            Text_body.text = $"<color=blue>{playerBody[1] + bodyArmor}</color>";
        }
        else if (playerBody[1] >= 66f)
        {
            Text_body.text = $"<color=green>{playerBody[1]}</color>";
        }
        else if (playerBody[1] >= 33f)
        {
            Text_body.text = $"<color=yellow>{playerBody[1]}</color>";
        }
        else
        {
            Text_body.text = $"<color=red>{playerBody[1]}</color>";
        }


        if (playerBody[2] >= 66f)
        {
            Text_leftArm.text = $"<color=green>{playerBody[2]}</color>";
        }
        else if (playerBody[2] >= 33f)
        {
            Text_leftArm.text = $"<color=yellow>{playerBody[2]}</color>";
        }
        else
        {
            Text_leftArm.text = $"<color=red>{playerBody[2]}</color>";
        }


        if (playerBody[3] >= 66f)
        {
            Text_leftLeg.text = $"<color=green>{playerBody[3]}</color>";
        }
        else if (playerBody[3] >= 33f)
        {
            Text_leftLeg.text = $"<color=yellow>{playerBody[3]}</color>";
        }
        else
        {
            Text_leftLeg.text = $"<color=red>{playerBody[3]}</color>";
        }


        if (playerBody[4] >= 66f)
        {
            Text_rightArm.text = $"<color=green>{playerBody[4]}</color>";
        }
        else if (playerBody[4] >= 33f)
        {
            Text_rightArm.text = $"<color=yellow>{playerBody[4]}</color>";
        }
        else
        {
            Text_rightArm.text = $"<color=red>{playerBody[4]}</color>";
        }


        if (playerBody[5] >= 66f)
        {
            Text_rightLeg.text = $"<color=green>{playerBody[5]}</color>";
        }
        else if (playerBody[4] >= 33f)
        {
            Text_rightLeg.text = $"<color=yellow>{playerBody[5]}</color>";
        }
        else
        {
            Text_rightLeg.text = $"<color=red>{playerBody[5]}</color>";
        }


        #endregion

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

            if (Random.Range(1, 100) < 27f)
            {
                // bleed out;

                print("bleed");

                bleeding = true;
            }
        }

    }
}
