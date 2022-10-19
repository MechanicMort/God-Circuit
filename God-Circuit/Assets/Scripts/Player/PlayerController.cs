using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;




[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject lantern;
    public GameObject testGun;
    public GameObject gunSpot;
    public GameObject motherBoard;
    public GameObject frontSpot;
    public GameObject backSpot;

    public Camera playerCam;
    public Transform playerCameraParent;
    CharacterController characterController;
    [Header("PlayerStats")]
    public float gunDrag = 5;
    public float StaminaMax = 100;
    public float hpMax = 100;
    public float shieldMax = 100;

    public float airJumps = 1;
    public float airJumpsMax = 1;
    public float dashLength = 100;
    public float dashCoolDown = 100;
    public float dashStamCost;

    public Image stamDisplay;
    public float Stamina;
    public float staminaDrain;
    public float staminaRecovery;
    public Image healthDisplay;
    public float hP;
    public float hPRecovery;
    public Image shieldDisplay;
    public float shield;
    public float shieldRecovery;
    public float speed;
    public float sprintSpeed = 16.5f;
    public float normalSpeed = 10.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    public float drainwait = 100;

    [Header("WeaponStats")]
    public float fireRateMod;
    public float damageMod;

    [HideInInspector]
    public bool canMove = true;
    public bool inBoardMode = false;
    public bool stamDroppedLow = true;
    public bool isDashing = false;
    public bool frontBack = true;

    void Start()
    {
        Stamina = StaminaMax;
        hP = hpMax;
        shield = shieldMax;
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(drainTimer());
        StartCoroutine(DashCoolDown());
        GetStats();

    }

    public void TakeDamage(float damage)
    {
        if (shield > 0 )
        {
            float leftovers = 0;
            shield -= damage;
            if (shield < 0)
            {
                leftovers = -shield;
                hP -= leftovers;
            }
        }
        else
        {
            hP -= damage;
        }
    }

    private IEnumerator drainTimer()
    {
        drainwait =  Mathf.Clamp(drainwait, 0, 100);
        yield return new WaitForSeconds(0.01f);
        drainwait -= 1;
        StartCoroutine(drainTimer());

    }
    private IEnumerator Dashing()
    {

        yield return new WaitForSeconds(0.1f);
        isDashing = false;
    }

    private IEnumerator DashCoolDown()
    {
        dashCoolDown = Mathf.Clamp(dashCoolDown, 0, 100);
        yield return new WaitForSeconds(0.01f);
        dashCoolDown -= 1;
        StartCoroutine(DashCoolDown());
    }

    private void Update()
    {
        if (!inBoardMode)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (frontBack)
            {
                frontBack = !frontBack;
                motherBoard.transform.position = frontSpot.transform.position;
                inBoardMode = true;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0.05f;
            }
            else
            {
                frontBack = !frontBack;
                motherBoard.transform.position = backSpot.transform.position;
                inBoardMode = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }

            motherBoard.GetComponent<MotherBoard>().inBuildMode = inBoardMode;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            lantern.SetActive(!lantern.activeInHierarchy);
        }
    }

    private void AirDash()
    {

    }

    private void Dash()
    {
        if (!isDashing && Stamina >= 10)
        {
            Stamina -= 10;
            isDashing = true;
            StartCoroutine(Dashing());
        }       
    }

    public void GetStats()
    {
        GameObject motherBoard = GameObject.FindGameObjectWithTag("MotherBoard");
        hpMax = motherBoard.GetComponent<MotherBoard>().hpMax;
        shieldMax = motherBoard.GetComponent<MotherBoard>().shieldMax;
        normalSpeed = motherBoard.GetComponent<MotherBoard>().normalSpeed;
        sprintSpeed = motherBoard.GetComponent<MotherBoard>().sprintSpeed;
        //clamp all stats after
    }

    public void Movement()
    {
        testGun.transform.position = Vector3.Lerp(testGun.transform.position, gunSpot.transform.position, gunDrag);
        testGun.transform.rotation = Quaternion.Lerp(testGun.transform.rotation, gunSpot.transform.rotation, gunDrag);
        stamDisplay.fillAmount = Stamina / StaminaMax;
        healthDisplay.fillAmount = hP / hpMax;
        shieldDisplay.fillAmount = shield / shieldMax;

        Stamina = Mathf.Clamp(Stamina, 0, StaminaMax);
        hP = Mathf.Clamp(hP, 0, hpMax);
        shield = Mathf.Clamp(shield, 0, shieldMax);

        if (Input.GetKey(KeyCode.LeftShift) && Stamina != 0 && !stamDroppedLow)
        {

            speed = sprintSpeed; //Mathf.Lerp(speed,sprintSpeed,0.02f);
            Stamina -= staminaDrain;
            drainwait = 100;
        }
        else
        {
            speed = normalSpeed;
            if (drainwait <= 0)
            {
                Stamina += staminaRecovery;
                if (Stamina <= 6)
                {
                    stamDroppedLow = true;
                }
                else
                {
                    stamDroppedLow = false;
                }
            }
        }
        if (characterController.isGrounded)
        {
            airJumps = airJumpsMax;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
          
            if (Input.GetKey(KeyCode.LeftControl) && dashCoolDown <= 0)
            {
                dashCoolDown = 100;
                drainwait = 100;
                Dash();
            }
            if (isDashing)
            {
                speed *= dashLength;
            }
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && Stamina >= 6)
            {
                Stamina -= 3;
                drainwait = 100;
                moveDirection.y = jumpSpeed;
            }
        }

        if (!characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && canMove && Stamina >= 5 && airJumps > 0)
            {
                airJumps -= 1;
                Stamina -= 5;
                drainwait = 100;
                moveDirection.y = jumpSpeed;

            }
            //use float for mid air jumps and also wall ruinning and wall jumps
        }

        moveDirection.y -= gravity * Time.deltaTime;

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
