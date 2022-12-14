using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;




[RequireComponent(typeof(CharacterController))]
public class BaseOverWorldController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject lantern;
    public Camera playerCam;
    public Transform playerCameraParent;

    CharacterController characterController;
    public float speed;
    public float jumpSpeed;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    [HideInInspector]
    public bool canMove = true;
    public bool hasTorch = false;
    public bool faster = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;


    }




    public void InvokeThings()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            print("Casting");
    
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {

                // debugHolder.transform.position = hit.point;
                if (hit.collider.GetComponent<InvokeInteraction>())
                {
                    hit.collider.GetComponent<InvokeInteraction>().InvokeTheInteraction();
                    print("Starting Invoke Chain");
                }
            }

        }
    }

    private void Update()
    {
        Movement();
        InvokeThings();
        if (Input.GetKeyDown(KeyCode.T) && hasTorch)
        {
            lantern.SetActive(!lantern.activeInHierarchy);
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
