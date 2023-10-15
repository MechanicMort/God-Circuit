using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject lantern;
    public GameObject testGun;
    public GameObject gunSpot;
    public MotherBoard motherBoard;
    public GameObject frontSpot;
    public GameObject backSpot;

    public Camera playerCam;
    public Transform playerCameraParent;
    CharacterController characterController;
    [Header("PlayerStats")]
    public float NoOfBuffs = 0;

    public float gunDrag = 5;
    public float StaminaMax = 0;
    public float hpMax = 0;
    public float shieldMax = 0;

    public float airJumps = 1;
    public float airJumpsMax = 1;
    public float dashLength = 25;
    public float dashCoolDown = 100;
    public float dashStamCost;

    public float Stamina;
    public float staminaDrain;
    public float staminaRecovery;

    public float hP;
    public float hPRecovery;

    public float shield;
    public float shieldRecovery;
    public float shieldRecoverySpeed = 100;
    public float stamRecoverySpeed = 100;
    public float speed;
    public float sprintSpeed = 16.5f;
    public float crouchSpeed = 6.5f;
    public float normalSpeed = 10.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    public float drainwait = 100;
    public float shieldDrainWait = 100;

    [Header("UITHINGIES")]
    public Image stamDisplay;

    public Image healthDisplay;

    public Image shieldDisplay;
    public TextMeshProUGUI hpAmount;
    public TextMeshProUGUI hpAmountMax;

    public TextMeshProUGUI shieldAmount;
    public TextMeshProUGUI shieldAmountMax;

    public TextMeshProUGUI staminaAmount;
    public TextMeshProUGUI staminaAmountMax;



    public Image powerDisplay;
    public Image currentPowerUsage;
    public TextMeshProUGUI currentPowerUsageAmount;
    public TextMeshProUGUI maxPower;


    public Image heatDisplay;
    public TextMeshProUGUI currentHeat;
    public TextMeshProUGUI heatMax;


    [Header("WeaponStats")]
    public float fireRateMod;
    public float damageMod;

    [HideInInspector]
    public bool canMove = true;
    public bool isCrouched = false;
    public bool inBoardMode = false;
    public bool stamDroppedLow = true;
    public bool isDashing = false;
    public bool frontBack = true;

    void Start()
    {
        motherBoard = GameObject.FindGameObjectWithTag("MotherBoard").GetComponent<MotherBoard>();
        Stamina = StaminaMax;
        hP = hpMax;
        shield = shieldMax;
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(drainTimer());
        StartCoroutine(DashCoolDown());

        motherBoard.PartSwap();

    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            shieldDrainWait = shieldRecoverySpeed;
            if (shield > 0)
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
        else
        {
            hP -= damage;
        }
    
    }

    private IEnumerator drainTimer()
    {
        drainwait =  Mathf.Clamp(drainwait, 0, stamRecoverySpeed);
        shieldDrainWait = Mathf.Clamp(shieldDrainWait, 0, shieldRecoverySpeed);
        yield return new WaitForSeconds(0.01f);
        drainwait -= 1;
        shieldDrainWait -= 1;
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

    private void UpdateUI()
    {


        //stamina bar
        stamDisplay.fillAmount = Stamina / StaminaMax;
        staminaAmount.text = Mathf.RoundToInt(Stamina).ToString();
        staminaAmountMax.text = Mathf.RoundToInt(StaminaMax).ToString();
        //shield bar
        shieldDisplay.fillAmount = shield / shieldMax;
        shieldAmount.text = Mathf.RoundToInt(shield).ToString();
        shieldAmountMax.text = Mathf.RoundToInt(shieldMax).ToString();
        // health bar
        healthDisplay.fillAmount = hP / hpMax;
        hpAmount.text = Mathf.RoundToInt(hP).ToString();
        hpAmountMax.text = Mathf.RoundToInt(hpMax).ToString();
        //power Bar
        maxPower.text = motherBoard.maxPower.ToString();
        currentPowerUsage.fillAmount = motherBoard.currentPower/ motherBoard.maxPower;
        currentPowerUsageAmount.text = Mathf.RoundToInt(motherBoard.currentPower).ToString();
        // heat bar
        heatMax.text = motherBoard.maxHeat.ToString();
        heatDisplay.fillAmount = motherBoard.totalHeat / motherBoard.maxHeat;
        currentHeat.text = Mathf.RoundToInt(motherBoard.totalHeat).ToString();
    }

    public void InvokeThings()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {

                // debugHolder.transform.position = hit.point;
                if (hit.collider.GetComponent<InvokeInteraction>())
                {
                    hit.collider.GetComponent<InvokeInteraction>();
                }
            }

        }
    }

    private void Update()
    {
        InvokeThings();
        UpdateUI();
        if (!inBoardMode)
        {
            Movement();
            Combat();

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



    private void Combat()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            print("Fire Weapon");
            motherBoard.FireWeapon();
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

        hpMax = motherBoard.GetComponent<MotherBoard>().hpMax;
        shieldMax = motherBoard.GetComponent<MotherBoard>().shieldMax;
        normalSpeed = motherBoard.GetComponent<MotherBoard>().normalSpeed;
        sprintSpeed = motherBoard.GetComponent<MotherBoard>().sprintSpeed;
        StaminaMax = motherBoard.GetComponent<MotherBoard>().StaminaMax;
        NoOfBuffs = motherBoard.GetComponent<MotherBoard>().NooFBuffs;

        staminaRecovery = motherBoard.staminaRecovery;
        shieldRecovery = motherBoard.shieldRecovery;
        hPRecovery = motherBoard.hPRecovery;


        print("Stats Changed");
        //clamp all stats after
    }

    public void Movement()
    {
        testGun.transform.position = Vector3.Lerp(testGun.transform.position, gunSpot.transform.position, gunDrag);
        testGun.transform.rotation = Quaternion.Lerp(testGun.transform.rotation, gunSpot.transform.rotation, gunDrag);

        if (shieldDrainWait <= 0)
        {
            shield += shieldRecovery * Time.deltaTime;
        }


        Stamina = Mathf.Clamp(Stamina, 0, StaminaMax);
        hP = Mathf.Clamp(hP, 0, hpMax);
        shield = Mathf.Clamp(shield, 0, shieldMax);

        if (Input.GetKey(KeyCode.LeftShift) && Stamina != 0 && !stamDroppedLow && !isCrouched)
        {

            speed = sprintSpeed; //Mathf.Lerp(speed,sprintSpeed,0.02f);
            Stamina -= staminaDrain * Time.deltaTime;
            drainwait = stamRecoverySpeed;
        }
        else
        {
            speed = normalSpeed;
            if (drainwait <= 0)
            {
                Stamina += staminaRecovery * Time.deltaTime;
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
          
            if (Input.GetKey(KeyCode.LeftControl) && dashCoolDown <= 0 && !isCrouched)
            {
                dashCoolDown = 100;
                drainwait = 100;
                Dash();
            }
            else if (Input.GetKey(KeyCode.C))
            {

                speed = crouchSpeed;
                isCrouched = true;
                transform.localScale = new Vector3(1, 0.7f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1.4f, 1);
                isCrouched = false;
                speed = normalSpeed;
            }
            if (isDashing)
            {
                speed *= dashLength;
            }
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && Stamina >= 6 && !isCrouched)
            {
                Stamina -= 3;
                drainwait = 100;
                moveDirection.y = jumpSpeed;
            }
        }

        if (!characterController.isGrounded)
        {
            isCrouched = false;
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
