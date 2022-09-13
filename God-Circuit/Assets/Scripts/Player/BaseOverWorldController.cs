using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;
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


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;


    }

  




    private void Update()
    {
        Movement();
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
                if (hit.transform.tag != "Train")
                {
                    transform.SetParent(null);
                    print("removing parent");
                }
            }
        }
      
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
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
