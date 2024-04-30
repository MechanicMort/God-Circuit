using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;




[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Event e;

    [Header("GameObjects")]
    public GameObject lantern;

    private BaseOverWorldController controller;

    public GameObject gunHolder;

    public GameObject[] myWeapons = new GameObject[3];
    public GameObject gunHolster;
    public bool hasWeaponOut = false;


    public GameObject gunSpot;
    public GameObject ADSSpot;
    public GameObject ADSSpotCrouched;
    public MotherBoard motherBoard;
    public GameObject frontSpot;
    public GameObject backSpot;

    public GameObject leftLean;
    public GameObject rightLean;
    public GameObject centrePos;
    
    public GameObject leftLeanCrouched;
    public GameObject rightLeanCrouched;
    public GameObject centrePosCrouched;

    public Camera playerCam;
    public Transform playerCameraParent;
    CharacterController characterController;
    [Header("PlayerStats")]
    public float rotateMod = 0;

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
    public float moveSpeedMod = 1;
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
    public GameObject UIPanel;
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
        controller = GetComponent<BaseOverWorldController>();
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

    public void OnEnable()
    {
        ToggleUI(true);
    }

    public void ToggleUI(bool onOrOff)
    {
        UIPanel.SetActive(onOrOff);
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

    private void SwapControllMode()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleUI(false);
            WeaponSwapping(KeyCode.Alpha4);
            controller.enabled = true;
            this.GetComponent<PlayerController>().enabled = false;
        }
    }

    private void Update()
    {

        SwapControllMode();
        InvokeThings();
        UpdateUI();
        if (!inBoardMode)
        {
            Combat();
            Movement();

        }

        if (Input.GetKeyDown(KeyCode.B) && !isCrouched)
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


    public bool WeaponEquip(GameObject weapon)
    {
        for (int i = 0;i < myWeapons.Length-1;i++)
        {
            if (myWeapons[i] == null)
            {
                myWeapons[i] = weapon;
                return true;
            }
        }
        return false;
    }

    private void ResetCurrentWeapon()
    {
        if (GameObject.FindGameObjectWithTag("CurrentWeapon"))
        {
            GameObject weaponManip = GameObject.FindGameObjectWithTag("CurrentWeapon");
            weaponManip.transform.parent = gunHolster.transform;
            weaponManip.GetComponent<Animator>().enabled = false;
            weaponManip.transform.tag = "Weapon";
            weaponManip.transform.position = Vector3.zero;
            weaponManip.transform.rotation = quaternion.identity;

        }
    }

    private IEnumerator AnimatorDelay(Animator aim)
    {
        aim.gameObject.transform.localPosition = Vector3.zero;
        aim.gameObject.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(0.01f);
        aim.enabled = true;
        motherBoard.WeaponSwap(aim.gameObject);
    }

    private void WeaponSwapping(KeyCode input)
    {
        if (input == (KeyCode.Alpha1) && myWeapons[0] != null)
        {
            print("WeaponsSwap");
            ResetCurrentWeapon();
            myWeapons[0].transform.parent = gunHolder.transform;
            myWeapons[0].transform.position = Vector3.zero;
            myWeapons[0].transform.rotation = Quaternion.identity;
            myWeapons[0].transform.tag = "CurrentWeapon";
            hasWeaponOut = true;
            StartCoroutine(AnimatorDelay(myWeapons[0].GetComponent<Animator>()));



        }
        else if (input == KeyCode.Alpha2 && myWeapons[1] != null)
        {
            ResetCurrentWeapon();
            myWeapons[1].transform.parent = gunHolder.transform;
            myWeapons[1].transform.position = Vector3.zero;
            myWeapons[1].transform.rotation = Quaternion.identity;
            myWeapons[1].GetComponent<Animator>().enabled = true;
            myWeapons[1].transform.tag = "CurrentWeapon";
            hasWeaponOut = true;
        }
        else if (input == KeyCode.Alpha3 && myWeapons[2] != null)
        {
            ResetCurrentWeapon();
            myWeapons[2].transform.parent = gunHolder.transform;
            myWeapons[2].transform.position = Vector3.zero;
            myWeapons[2].transform.rotation = Quaternion.identity;
            myWeapons[2].GetComponent<Animator>().enabled = true;
            myWeapons[2].transform.tag = "CurrentWeapon";
            hasWeaponOut = true;
        }

        else if (input == KeyCode.Alpha4)
        {

            print("Holstering");
            ResetCurrentWeapon();
            hasWeaponOut = false;
        }

    }

    private void OnGUI()
    {

        e = Event.current;

    }

    private void Combat()
    {
        Lean();
        ADS();

        if (e != null)
        {
   
        WeaponSwapping(e.keyCode);
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

    private void ADS()
    {

        if (Input.GetKey(KeyCode.Mouse1) && isCrouched)
        {
            gunHolder.transform.position = ADSSpotCrouched.transform.position;// Vector3.Lerp(testGun.transform.position, ADSSpot.transform.position, 0.5f);
            gunHolder.transform.rotation = ADSSpotCrouched.transform.rotation; //Quaternion.Lerp(testGun.transform.rotation, ADSSpot.transform.rotation, 0.5f);
            moveSpeedMod = 0.6f;
        }  
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            gunHolder.transform.position = ADSSpot.transform.position;// Vector3.Lerp(testGun.transform.position, ADSSpot.transform.position, 0.5f);
            gunHolder.transform.rotation = ADSSpot.transform.rotation; //Quaternion.Lerp(testGun.transform.rotation, ADSSpot.transform.rotation, 0.5f);
            moveSpeedMod = 0.6f;
        }
        else
        {
            gunHolder.transform.position = gunSpot.transform.position; // Vector3.Lerp(testGun.transform.position, gunSpot.transform.position, 0.5f);
            gunHolder.transform.rotation = gunSpot.transform.rotation;// Quaternion.Lerp(testGun.transform.rotation, gunSpot.transform.rotation, 0.5f);
            moveSpeedMod = 1f;
        }
    }


    private void Lean()
    {
        if (!isCrouched)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, leftLean.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, leftLean.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, 20f, 0.5f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, rightLean.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, rightLean.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, -20f, 0.5f);
            }
            else
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, centrePos.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, centrePos.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, 0, 0.5f);

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, leftLeanCrouched.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, leftLeanCrouched.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, 20f, 0.5f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, rightLeanCrouched.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, rightLeanCrouched.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, -20f, 0.5f);
            }
            else
            {
                playerCameraParent.transform.position = Vector3.Lerp(playerCameraParent.transform.position, centrePosCrouched.transform.position, 0.5f);
                playerCameraParent.transform.rotation = Quaternion.Lerp(playerCameraParent.transform.rotation, centrePosCrouched.transform.rotation, 0.5f);
                rotateMod = Mathf.Lerp(rotateMod, 0, 0.5f);

            }
        }

     
    }

    public void Movement()
    {
   

   

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

            //

            //if ((Input.GetKey(KeyCode.C)))
            //{
            //    //make crouch bool true
            //    //ToDo: make toggle coruch
            //}

            else if (Input.GetKey(KeyCode.C))
            {

                speed = crouchSpeed;
                isCrouched = true;
                transform.localScale = new Vector3(1, 0.7f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1f, 1);
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

        characterController.Move(moveDirection * Time.deltaTime * moveSpeedMod);

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);

            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, rotateMod);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }
      
}
