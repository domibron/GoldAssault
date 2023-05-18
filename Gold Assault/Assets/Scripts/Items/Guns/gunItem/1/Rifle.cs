using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    private Animator animator;
    private bool equipped = false;

    private float localTime = 0f;
    private float delay = 0f;

    private Transform player;
    private Camera cam;

    public AudioClip audioClip;
    public AudioSource audioSource;

    public GameObject bulletLine;

    public int currrentAmmoMag = 0;
    public List<int> ammoMags = new List<int>();

    private float inputTimeCheck = 0f;
    private float calcTime = 0f;

    private DisplayText displayText;
    private PlayerInteractionText interactionText;

    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;

        animator = GetComponent<Animator>();


        for (int i = 0; i <= ((GunInfo)itemInfo).maxAmmoMags - 1; i++)
        {
            ammoMags.Add(((GunInfo)itemInfo).maxAmmoInClip);
        }

        displayText = new DisplayText();
        GameObject[] _tempGO = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in _tempGO)
        {
            if (go.name.Contains("Player")) interactionText = go.GetComponent<PlayerInteractionText>();
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

        if (Input.GetMouseButton(0))
        {
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            calcTime = Time.time;
            // print(calcTime);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            // print(Time.time + " " + (Time.time - calcTime));
            if (Time.time - calcTime < 1)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            inputTimeCheck += Time.deltaTime;
            if (inputTimeCheck > 1f)
            {
                string _tempStr = "";
                for (int i = 0; i < ammoMags.Count; i++)
                {
                    if (currrentAmmoMag == i)
                    {
                        _tempStr += $"<color=blue>[{ammoMags[i]}]</color> ";
                    }
                    else
                    {
                        _tempStr += $"<color=white>[{ammoMags[i]}]</color> ";
                    }
                }

                displayText.text = _tempStr;
                displayText.priority = 1;

                if (!interactionText.IsSubTextInTheList(displayText))
                    interactionText.AddSubText(displayText);

            }
        }
        else
        {
            if (inputTimeCheck != 0) inputTimeCheck = 0;
        }
    }

    private IEnumerator Reload()
    {
        animator.SetTrigger("Reload");

        isReloading = true;

        yield return new WaitForSeconds(((GunInfo)itemInfo).reloadSpeed);

        if (ammoMags.Count > 1)
        {
            int _tempInt = currrentAmmoMag;

            if (ammoMags[_tempInt] <= 0 && currrentAmmoMag < _tempInt)
            {
                ammoMags.RemoveAt(_tempInt);
                if (currrentAmmoMag >= ammoMags.Count - 1)
                {
                    currrentAmmoMag = 0;
                }
                else
                {
                    currrentAmmoMag++;
                }
            }
            else if (ammoMags[_tempInt] <= 0 && currrentAmmoMag >= _tempInt)
            {
                if (currrentAmmoMag == ammoMags.Count - 1)
                {
                    currrentAmmoMag = 0;
                }
                else
                {
                    // silly but it gets the point accross.
                    currrentAmmoMag = currrentAmmoMag;
                }
                ammoMags.RemoveAt(_tempInt);
            }
            else if (ammoMags[_tempInt] > 0 && currrentAmmoMag == ammoMags.Count - 1)
            {
                currrentAmmoMag = 0;
            }
            else if (ammoMags[_tempInt] > 0)
            {
                currrentAmmoMag++;
            }

            print(currrentAmmoMag + " ammo: " + ammoMags[currrentAmmoMag]);

        }
        else
        {
            print("last mag");
        }

        isReloading = false;
    }

    private void shoot()
    {
        if (localTime <= ((GunInfo)itemInfo).fireRate && ammoMags[currrentAmmoMag] > 0 && !isReloading)
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
                GameObject _go = Instantiate(bulletLine, Vector3.zero, Quaternion.identity);
                _go.GetComponent<BulletSmoke>().CreateLine(player.position, hit.point, 1f);

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
