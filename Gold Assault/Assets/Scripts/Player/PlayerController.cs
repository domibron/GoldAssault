using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface.IInteractable;

public class PlayerController : MonoBehaviour
{
    private CharacterController CC;

    private GameObject cam;

    [SerializeField] private float speed = 6f;

    [SerializeField] private float jumpHeight = 3f;

    //gravity
    private bool isGrounded = false;

    private Vector3 velocity;

    [SerializeField] private float gravity = -9.81f;

    // camera
    private float xRotation;
    [SerializeField] private float sensitivity = 1f;

    [SerializeField] private GameObject groundCheck;


    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        cam = transform.Find("Camera Holder").gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //move /= speedNurf;

        // this is to check if the player is holding shift bit not left control.
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            CC.Move(move.normalized * 2 * speed * Time.smoothDeltaTime);

        // this is to check if the player is not holding both keys.
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            CC.Move(move.normalized * speed * Time.smoothDeltaTime);

        // this is to check if the player is just holding left control.
        else if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            CC.Move(move.normalized / 2 * speed * Time.smoothDeltaTime);

        //this is to check if the player is holding both keys.
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            CC.Move(move.normalized / 1.5f * speed * Time.smoothDeltaTime);

        // this is to catch and unpinplimented checks.
        else
            Debug.LogError("character vars not set");



        if (Input.GetKey(KeyCode.LeftControl)) // REPLACE WITH TIME OTHERWISE ISSUES WILL OCCUR
            transform.localScale = new Vector3(transform.localScale.x,
            Mathf.Lerp(transform.localScale.y, 0.5f, transform.localScale.y * 0.2f), transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x,
            Mathf.Lerp(transform.localScale.y, 1f, transform.localScale.y * 0.2f), transform.localScale.z);

        // player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        CC.Move(velocity * Time.deltaTime);

        //Camera
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.Rotate(Vector3.up * mouseX);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // isGrounded = Physics.CheckSphere(groundCheck.transform.position, CC.radius - 0.05f, 3);

        // ground check
        isGrounded = CC.isGrounded;


        // RaycastHit hit;
        // float extention;


        // Physics.Raycast(transform.position, Vector3.down, out hit, CC.height / 2 + 0.5f);
        // if (hit.normal != Vector3.up)
        //     extention = maxDistance;
        // else
        //     extention = minDistance;

        // isGrounded = Physics.Raycast(transform.position, Vector3.down, CC.height / 2 + extention);


        // Vector3 groundCheckPos = CC.center - new Vector3(0, CC.height, 0);

        // RaycastHit hit;

        // isGrounded = Physics.CheckSphere(groundCheck.transform.position, CC.radius - 0.05f);
        // if (Physics.SphereCast(groundCheck.transform.position, CC.radius - 0.05f, -transform.up, out hit, CC.radius - 0.05f))
        // {
        //     print("yes");
        //     if (hit.transform.gameObject.layer == 3)
        //     {
        //         isGrounded = true;
        //         print("ground");
        //     }
        //     else
        //     {
        //         isGrounded = false;
        //         print("failed ground");
        //     }
        // }


        // Interactions

        RaycastHit Ihit;
        bool wasHit = Physics.Raycast(cam.transform.position, cam.transform.forward, out Ihit, 2.2f);

        if (wasHit && Ihit.collider.gameObject.GetComponent<IInteractable>() != null)
        {
            Ihit.collider.gameObject.GetComponent<IInteractable>().lookingAt();
            if (Input.GetKeyDown(KeyCode.F))
            {
                Ihit.collider.gameObject.GetComponent<IInteractable>().RunInteract();
            }
        }
    }

    public void SetGrounded(bool _isGrounded)
    {
        isGrounded = _isGrounded;
    }
}
