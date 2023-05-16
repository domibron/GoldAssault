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

    public int currrentAmmoMag = 0;
    public List<int> ammoMags = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;

        animator = GetComponent<Animator>();

        for (int i = 0; i <= ((GunInfo)itemInfo).maxAmmoMags; i++)
        {
            ammoMags.Add(((GunInfo)itemInfo).maxAmmoInClip);
        }


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

    private void Reload()
    {
        if (ammoMags.Count > 0)
        {
            int _tempInt = currrentAmmoMag;

            if (ammoMags[_tempInt] <= 0)
            {
                ammoMags.RemoveAt(_tempInt);
            }

            // if (currrentAmmoMag)

            if (currrentAmmoMag == ammoMags.Count - 1) currrentAmmoMag = 0;
        }
    }

    private void shoot()
    {
        if (localTime <= ((GunInfo)itemInfo).fireRate && ammoMags[currrentAmmoMag] > 1)
        {
            localTime += ((GunInfo)itemInfo).fireRate;
            animator.SetTrigger("Fire");

            audioSource.clip = audioClip;
            audioSource.Play(); // get the audio source in code.

            ammoMags[currrentAmmoMag] -= 1;

            int layer = 9;
            // layer = 1 << layer; // makes the layer 9 to be hit.
            layer = (1 << layer) | (1 << 7);
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
