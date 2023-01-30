using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;

    private Vector3 moveDir;

    private GameObject cam;
    private GameObject groundCheck;

    private float y;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = transform.Find("Camera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        cc.SimpleMove(moveDir * 4f);


        y -= Input.GetAxisRaw("Mouse Y") * 1f;
        x += Input.GetAxisRaw("Mouse X") * 1f;

        cc.transform.rotation = Quaternion.Euler(0, x, 0);
        cam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(y, -90f, 90f), 0, 0);

    }
}
