using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock17 : Gun // index ID is 2 because it is a pistol
{

    private Animator animator;
    private bool equipped = false;

    private float localTime = 0f;
    private float delay = 0f;

    private Transform player;
    private Camera cam;

    public AudioClip audioClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;

        animator = GetComponent<Animator>();
        equipped = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        if (localTime > 0) localTime -= Time.unscaledDeltaTime;
        else localTime = 0;

        if (gameObject.activeSelf && !equipped)
        {
            equipped = true;
            animator.SetTrigger("Equip");
        }
        else if (!gameObject.activeSelf)
        {
            equipped = false;
        }

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("ADS", true);
        }
        else
        {
            animator.SetBool("ADS", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (localTime <= ((GunInfo)itemInfo).fireRate)
        {
            localTime += ((GunInfo)itemInfo).fireRate;
            animator.SetTrigger("Fire");

            audioSource.clip = audioClip;
            audioSource.Play(); // get the audio source in code.

            int layer = 9;
            layer = 1 << layer; // makes the layer 9 to be hit.
                                // layer = (1 << layer) | (1 << 1);
            layer = ~layer; // inverts so that the body can be hit.

            // shoot
            // Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, layer) && hit.transform.tag != "Player")
            {
                hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);

                if (hit.collider.gameObject.layer == 8)
                {
                    Wall wall = hit.collider.gameObject.GetComponent<Wall>();
                    wall.AddBulletHole(ray, 0.1f);
                }
            }

            foreach (GameObject go in PlayerRefernceItems.current.AINoiseAlertSubs)
            {
                go.GetComponent<INoiseAlert>().NoiseMade(player.position);
            }
        }


    }

    public override void UseMouse0()
    {
        throw new System.NotImplementedException();
    }

    public override void UseRKey()
    {
        throw new System.NotImplementedException();
    }

}
