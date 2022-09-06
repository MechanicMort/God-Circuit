using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherBoard : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] GPUSlots;
    public GameObject[] CPUSlots;
    public GameObject[] RamSlots;
    public GameObject[] powerSupplySlots;
    public GameObject OutPutDevice;
    public GameObject VirusProtection;

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
    [Header("WeaponsStats")]
    public float maxPower;
    public float currentPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DrainPower(float drain)
    {
        currentPower -= drain;
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
    }

    public void FireWeapon(GameObject projectile)
    {
        OutPutDevice.GetComponent<BasicRangedWeapon>().FireWeapon(projectile);
    }

    public void AddPower(float power)
    {
        currentPower += power;
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
