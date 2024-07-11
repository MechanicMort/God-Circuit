using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;




[RequireComponent(typeof(CharacterController))]


public class BaseOverWorldController : MonoBehaviour
{
    public Vector3 phoneRat;
    
    [Header("GameObjects")]
    private PlayerController playerController;
    public GameObject lantern;
    public GameObject debugHolder;
    public GameObject phone;
    public GameObject phoneUpSpot;
    public GameObject phoneDownSpot;
    public Camera playerCam;
    public Transform playerCameraParent;
    public GameObject[] inventory = new GameObject[8];
    

    CharacterController characterController;
    [Header("Nums")]
    public float speed;
    public float jumpSpeed;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    public bool canMove = true;
    [HideInInspector]
    public bool hasTorch = false;
    public bool hasPhone = false;
    public bool phoneIsDown = false;
    public bool faster = false;
    public bool canTurn = false;
    public bool atWork = true;


    void Start()
    {
        
        playerController = GetComponent<PlayerController>();
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;     
    }


    private void SwapControllMode()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            playerController.enabled = true;
            this.GetComponent<BaseOverWorldController>().enabled = false;
        }
    }


    private void Update()
    {
        SwapControllMode();
        Movement();
        if (Input.GetKeyDown(KeyCode.T) && hasTorch)
        {
            lantern.SetActive(!lantern.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.I) && hasPhone)
        {
                phoneIsDown = !phoneIsDown;
        }
        if ( hasPhone)
        {
            if (phoneIsDown)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    phone.GetComponent<Phone>().OnOff();
                }
                Time.timeScale = 0.05f;
                Cursor.lockState = CursorLockMode.Confined;
                phone.transform.position = phoneUpSpot.transform.position;
                phone.transform.rotation = phoneUpSpot.transform.rotation;
                canMove = false;
                phone.transform.Rotate(phoneRat);
            }
            else
            {
                phone.GetComponent<Phone>().power = false;
                Time.timeScale = 1f;
                canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                phone.transform.position = phoneDownSpot.transform.position;
                phone.transform.rotation = phoneDownSpot.transform.rotation;
            }
        }
    }


    public void Movement()
    {
        if (transform.parent)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag != "Train" && hit.transform.tag != "Elevator")
                {
                    //transform.SetParent(null);
                   // print("removing parent");
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

            faster = true;
        }
        else
        {
            faster = false;
        }
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);


            float curSpeedX;
            float curSpeedY;
            if (faster)
            {
                 curSpeedX = canMove ? speed * 1.3f * Input.GetAxis("Vertical") : 0;
                 curSpeedY = canMove ? speed * 1.3f * Input.GetAxis("Horizontal") : 0;
            }
            else
            {
                 curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
                 curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            }

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
          
        }
        moveDirection.y -= gravity * Time.deltaTime;
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }

}
