using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public TMP_Text Text_blood;

    [Space]
    public Image Image_head;
    public Image Image_torso;
    public Image Image_leftArm;
    public Image Image_leftLeg;
    public Image Image_rightArm;
    public Image Image_rightLeg;

    [Space]
    public Image Image_helmet;
    public Image Image_bodyarmor;

    [Space]
    public Sprite Sprite_torso_black;
    public Sprite Sprite_torso_yellow;
    public Sprite Sprite_torso_orange;
    public Sprite Sprite_torso_red;

    [Space]
    public Sprite Sprite_head_black;
    public Sprite Sprite_head_yellow;
    public Sprite Sprite_head_orange;
    public Sprite Sprite_head_red;

    [Space]
    public Sprite Sprite_leftArm_black;
    public Sprite Sprite_leftArm_yellow;
    public Sprite Sprite_leftArm_orange;
    public Sprite Sprite_leftArm_red;

    [Space]
    public Sprite Sprite_leftLeg_black;
    public Sprite Sprite_leftLeg_yellow;
    public Sprite Sprite_leftLeg_orange;
    public Sprite Sprite_leftLeg_red;

    [Space]
    public Sprite Sprite_rightArm_black;
    public Sprite Sprite_rightArm_yellow;
    public Sprite Sprite_rightArm_orange;
    public Sprite Sprite_rightArm_red;

    [Space]
    public Sprite Sprite_rightLeg_black;
    public Sprite Sprite_rightLeg_yellow;
    public Sprite Sprite_rightLeg_orange;
    public Sprite Sprite_rightLeg_red;

    [Space]
    public float maxHealth = 100f;

    public float bloodLevel = 100f;

    public float helmet = 100f;
    public float bodyArmor = 100f;

    private float stageOneHealth = 80f;
    private float stageTwoHealth = 50f;
    private float stageThreeHealth = 20f;


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
    private float bleedMultipliyer = 0f;

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
            GameManager.current.Reload();

        }

        if (playerBody[1] <= 0)
        {
            // player is dead
            GameManager.current.Reload();
        }

        if (bloodLevel <= 0)
        {
            // player is dead
            GameManager.current.Reload();
        }

        if (bleeding)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                bleeding = false;
                bleedMultipliyer = 0f;

            }

            bloodLevel -= Time.deltaTime * 3f * bleedMultipliyer;


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
            if (!Image_helmet.gameObject.activeSelf)
                Image_helmet.gameObject.SetActive(true);
        }
        else
        {
            if (Image_helmet.gameObject.activeSelf)
                Image_helmet.gameObject.SetActive(false);
        }

        if (bodyArmor > 0)
        {
            if (!Image_bodyarmor.gameObject.activeSelf)
                Image_bodyarmor.gameObject.SetActive(true);
        }
        else
        {
            if (Image_bodyarmor.gameObject.activeSelf)
                Image_bodyarmor.gameObject.SetActive(false);
        }

        if (playerBody[0] >= stageOneHealth)
        {
            Image_head.sprite = Sprite_head_black;
        }
        else if (playerBody[0] >= stageTwoHealth)
        {
            Image_head.sprite = Sprite_head_yellow;
        }
        else if (playerBody[0] >= stageThreeHealth)
        {
            Image_head.sprite = Sprite_head_orange;
        }
        else
        {
            Image_head.sprite = Sprite_head_red;
        }


        if (playerBody[1] >= stageOneHealth)
        {
            Image_torso.sprite = Sprite_torso_black;
        }
        else if (playerBody[1] >= stageTwoHealth)
        {
            Image_torso.sprite = Sprite_torso_yellow;
        }
        else if (playerBody[1] >= stageThreeHealth)
        {
            Image_torso.sprite = Sprite_torso_orange;
        }
        else
        {
            Image_torso.sprite = Sprite_torso_red;
        }


        if (playerBody[2] >= stageOneHealth)
        {
            Image_leftArm.sprite = Sprite_leftArm_black;
        }
        else if (playerBody[2] >= stageTwoHealth)
        {
            Image_leftArm.sprite = Sprite_leftArm_yellow;
        }
        else if (playerBody[2] >= stageThreeHealth)
        {
            Image_leftArm.sprite = Sprite_leftArm_orange;
        }
        else
        {
            Image_leftArm.sprite = Sprite_leftArm_red;
        }


        if (playerBody[3] >= stageOneHealth)
        {
            Image_leftLeg.sprite = Sprite_leftLeg_black;
        }
        else if (playerBody[3] >= stageTwoHealth)
        {
            Image_leftLeg.sprite = Sprite_leftLeg_yellow;
        }
        else if (playerBody[3] >= stageThreeHealth)
        {
            Image_leftLeg.sprite = Sprite_leftLeg_orange;
        }
        else
        {
            Image_leftLeg.sprite = Sprite_leftLeg_red;
        }


        if (playerBody[4] >= stageOneHealth)
        {
            Image_rightArm.sprite = Sprite_rightArm_black;
        }
        else if (playerBody[4] >= stageTwoHealth)
        {
            Image_rightArm.sprite = Sprite_rightArm_yellow;
        }
        else if (playerBody[4] >= stageThreeHealth)
        {
            Image_rightArm.sprite = Sprite_rightArm_orange;
        }
        else
        {
            Image_rightArm.sprite = Sprite_rightArm_red;
        }


        if (playerBody[5] >= stageOneHealth)
        {
            Image_rightLeg.sprite = Sprite_rightLeg_black;
        }
        else if (playerBody[4] >= stageTwoHealth)
        {
            Image_rightLeg.sprite = Sprite_rightLeg_yellow;
        }
        else if (playerBody[4] >= stageThreeHealth)
        {
            Image_rightLeg.sprite = Sprite_rightLeg_orange;
        }
        else
        {
            Image_rightLeg.sprite = Sprite_rightLeg_red;
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

                bleedMultipliyer += 1;
            }
        }

    }
}
