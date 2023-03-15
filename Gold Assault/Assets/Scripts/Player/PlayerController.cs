using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface.IInteractable;
using UnityEngine.UI;

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
    private float yRotation;
    private float xRotation;

    [SerializeField] private float sensitivity = 1f;

    [SerializeField] private GameObject groundCheck;

    public GameObject rappellingObject = null;
    public bool canRappel = false;
    public Vector2 lowerLimit;
    public Vector2 upperLimit;
    public GameObject targPos;
    private bool isRappelling = false;
    private bool waiting = false;

    [SerializeField] private GameObject rappelInteraction;
    [SerializeField] private Image rappelImage;

    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        cam = transform.Find("Camera Holder").gameObject;

        Cursor.lockState = CursorLockMode.Locked;

        rappelInteraction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");



        //move /= speedNurf;

        if (canRappel && !isRappelling)
        {
            rappelInteraction.SetActive(true);
            rappelImage.fillAmount = currentTime;
            if (Input.GetKey(KeyCode.F))
            {
                //print(currentTime);
                currentTime += Time.deltaTime;
                if (currentTime >= 1f)
                {
                    isRappelling = true;
                    currentTime = 0f;

                    Transform parentT = rappellingObject.transform.parent.transform;
                    // float rot = 0;

                    // if (parentT.eulerAngles.y < 90)
                    // {
                    //     rot = 90 - parentT.eulerAngles.y;
                    //     rot = 360 - rot;
                    // }
                    // else
                    // {
                    //     rot = parentT.eulerAngles.y - 90;
                    // }

                    transform.rotation = Quaternion.Euler(-90, parentT.eulerAngles.y, 0);
                    //cam.transform.localRotation = Quaternion.Euler(0, rot, 0);

                    transform.SetParent(rappellingObject.transform);

                    //ChangePositionController(new Vector3(transform.position.x, transform.position.y, rappellingObject.transform.position.z));
                    ChangeLocalPositionController(new Vector3(transform.localPosition.x, transform.localPosition.y, rappellingObject.transform.localPosition.z - (CC.height / 4)));



                }
            }
            else
            {
                currentTime = 0f;
            }
        }
        else if (!canRappel && !isRappelling)
        {
            rappelInteraction.SetActive(false);
        }

        if (!isRappelling)
        {
            Vector3 move = transform.right * x + transform.forward * z;

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


            if (Input.GetKey(KeyCode.LeftControl)) // REPLACE WITH TIME OTHERWISE ISSUES WILL OCCUR - the lerps
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
        }
        else if (isRappelling)
        {
            Vector3 move = Vector3.zero;
            Transform RepelT = rappellingObject.transform;
            if (transform.localPosition.x <= upperLimit.x && transform.localPosition.y <= upperLimit.y && transform.localPosition.x >= lowerLimit.x && transform.localPosition.y >= lowerLimit.y)
            {
                move = transform.right * x + transform.forward * z;
            }
            else if (transform.localPosition.x > upperLimit.x)
            {
                ChangeLocalPositionController(new Vector3(upperLimit.x, transform.localPosition.y, transform.localPosition.z));
            }
            else if (transform.localPosition.y > upperLimit.y)
            {
                ChangeLocalPositionController(new Vector3(transform.localPosition.x, upperLimit.y, transform.localPosition.z));
            }
            else if (transform.localPosition.x < lowerLimit.x)
            {
                ChangeLocalPositionController(new Vector3(lowerLimit.x, transform.localPosition.y, transform.localPosition.z));
            }
            else if (transform.localPosition.y < lowerLimit.y)
            {
                ChangeLocalPositionController(new Vector3(transform.localPosition.x, lowerLimit.y, transform.localPosition.z));
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 180, transform.eulerAngles.y, transform.eulerAngles.z + 180);

            }

            CC.Move(move.normalized * speed * Time.smoothDeltaTime);

            velocity.y = 0f;

            rappelImage.fillAmount = currentTime;
            if (Input.GetKey(KeyCode.F))
            {
                //print(currentTime);
                currentTime += Time.deltaTime;
                if (currentTime >= 1f)
                {
                    if (transform.localPosition.y >= upperLimit.y - 2 || waiting)
                    {
                        waiting = true;
                        DismountAtRoof();
                        //ChangeLocalPositionController(new Vector3(transform.localPosition.x, upperLimit.y + (CC.height - 1), 2));
                    }
                    else if (isRappelling && !waiting)
                    {
                        currentTime = 0f;
                        transform.rotation = Quaternion.Euler(0, rappellingObject.transform.eulerAngles.y, 0);
                        cam.transform.localRotation = Quaternion.Euler(0, 0, 0);

                        
                        //print(transform.localPosition.y >= upperLimit.y - 2);
                        //print(transform.localPosition.y + " " + upperLimit.y);


                        transform.SetParent(null);
                        isRappelling = false;

                    }


                }
            }
            else
            {
                currentTime = 0f;
            }
        }






        CC.Move(velocity * Time.deltaTime);

        //Camera
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        if (!isRappelling) // put in the uper if statement
        {
            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);


            transform.Rotate(Vector3.up * mouseX);
            cam.transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        }
        else if (isRappelling)
        {


            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);


            xRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);


        }

        // ground check
        isGrounded = CC.isGrounded;




        // Transform parentT = rappellingObject.transform.parent.transform;
        // float minRot = 0;
        // float maxRot = 0;

        // if (parentT.eulerAngles.y > 90) // <
        // {
        //     float temp;
        //     temp = 90 - parentT.eulerAngles.y;
        //     minRot = 360 - temp;
        //     maxRot = parentT.eulerAngles.y - 90;
        // }
        // else if (parentT.eulerAngles.y < 90) // >
        // {
        //     float temp;
        //     temp = 360 - parentT.eulerAngles.y;
        //     maxRot = 90 - temp;
        //     minRot = parentT.eulerAngles.y + 90;
        // }
        // else
        // {
        //     maxRot = parentT.eulerAngles.y + 90;
        //     minRot = parentT.eulerAngles.y - 90;
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

    public void DismountAtRoof()
    {
        StartCoroutine(GetOffAtRoofRappel());
    }

    IEnumerator GetOffAtRoofRappel()
    {
        Vector3 targPos = new Vector3(transform.localPosition.x, upperLimit.y + (CC.height / 2), rappellingObject.transform.localPosition.z + 2);
        //targPos = transform.TransformPoint(targPos);
        //print(new Vector3(transform.localPosition.x, upperLimit.y + CC.height, 2));
        CC.enabled = false;
        yield return null;
        transform.localPosition = targPos;
        targPos = transform.position;
        yield return null;
        CC.enabled = true;

        Debug.LogAssertion("wait");
        yield return null;

        currentTime = 0f;
        transform.rotation = Quaternion.Euler(0, rappellingObject.transform.eulerAngles.y, 0);
        cam.transform.localRotation = Quaternion.Euler(0, 0, 0);

        print(transform.localPosition.y >= upperLimit.y - 2);
        print(transform.localPosition.y + " " + upperLimit.y);

        yield return null;

        while (transform.position != targPos)
        {
            CC.enabled = false;
            transform.position = targPos;
            yield return null;
        }

        CC.enabled = true;

        transform.SetParent(null);

        waiting = false;

        isRappelling = false;

    }

    public void SetGrounded(bool _isGrounded)
    {
        isGrounded = _isGrounded;
    }

    public void ChangePositionController(GameObject targetObj)
    {
        StartCoroutine(ChangePos(targetObj));
        // print("A");
    }

    public void ChangePositionController(Transform targetObj)
    {
        StartCoroutine(ChangePos(targetObj));
        // print("B");
    }

    public void ChangePositionController(Vector3 targetObj)
    {
        StartCoroutine(ChangePos(targetObj));
        // print("C");
    }

    IEnumerator ChangePos(GameObject targetObj)
    {
        CC.enabled = false;
        yield return null;
        transform.position = targetObj.transform.position;
        yield return null;
        CC.enabled = true;
        // print("a");
    }

    IEnumerator ChangePos(Transform targetObj)
    {
        CC.enabled = false;
        yield return null;
        transform.position = targetObj.position;
        yield return null;
        CC.enabled = true;
        // print("b");
    }

    IEnumerator ChangePos(Vector3 targetObj)
    {
        CC.enabled = false;
        yield return null;
        transform.position = targetObj;
        yield return null;
        CC.enabled = true;
        // print("c");
    }

    public void ChangeLocalPositionController(Vector3 targetObj)
    {
        StartCoroutine(ChangeLocalPos(targetObj));
        // print("D");
    }

    IEnumerator ChangeLocalPos(Vector3 targetObj)
    {
        CC.enabled = false;
        yield return null;
        transform.localPosition = targetObj;
        yield return null;
        CC.enabled = true;
        // print("d");
    }
}
