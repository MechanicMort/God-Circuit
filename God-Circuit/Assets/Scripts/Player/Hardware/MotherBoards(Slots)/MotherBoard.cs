using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MotherBoard : MonoBehaviour
{
    public PlayerController playerController;

    [Header("Components")]
    public GameObject[] GPU;
    public int GPUsInstalled;
    public GameObject[] CPU;
    public int CPUsInstalled;
    public GameObject[] Ram;
    public int RamInstalled;
    public GameObject[] PSU;
    public int PSUInstalled;
    public GameObject HUD;
    public int HUDInstalled;
    public GameObject OutPutDevice;
    public int OutPutDeviceInstalled;
    public GameObject VirusProtection;
    public int VirusProtectionInstalled;

    [Header("BoardManagement")]
    public GameObject[] Inventory = new GameObject[6];
    public GameObject[] InventorySpaces = new GameObject[6];
    public GameObject[] GPUSlots;
    public GameObject[] CPUSlots;
    public GameObject[] RamSlots;
    public GameObject[] PSUSlots;
    public GameObject heldComponent;

    [Header("PlayerStats")]
    public float gunDrag = 5;
    public float StaminaMax = 100;
    public float hpMax = 100;
    public float shieldMax = 100;
    public float dashStamCost;
    public float staminaDrain;
    public float staminaRecovery;
    public float hPRecovery;
    public float shieldRecovery;
    [Header("Movement")]
    public float airJumps = 1;
    public float airJumpsMax = 1;
    public float dashLength = 100;
    public float dashCoolDown = 100;
    public float speed;
    public float sprintSpeed = 16.5f;
    public float normalSpeed = 10.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float drainwait = 100;
    [Header("Power")]
    public float maxPower;
    public float currentPower;
    [Header("Buffs")]
    public float NooFBuffs;
    public Buff[] buffs;
    [Header("Bools")]
    public bool inBuildMode;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PartSwap();
    }

    public void PartSwap()
    {
      NooFBuffs = 0;
      StaminaMax = 10;
      hpMax = 10;
      shieldMax = 5;
      dashStamCost = 15;
      staminaDrain = 3;
      staminaRecovery = 0.09f;
      hPRecovery = 0;
      shieldRecovery = 0.09f;
      maxPower= 15;
        GPUsInstalled = 0;
        CPUsInstalled = 0;
        RamInstalled = 0;
        VirusProtectionInstalled = 0;
        HUDInstalled = 0;
        OutPutDeviceInstalled = 0;
        for (int i = 0; i < GPUSlots.Length; i++)
        {
            if (GPUSlots[i].GetComponentInChildren<UIComponentSwap>().component != null)
            {
                GPU[GPUsInstalled] = GPUSlots[i].GetComponentInChildren<UIComponentSwap>().component;
                GPUsInstalled += 1;
            }
        }
        for (int i = 0; i < RamSlots.Length; i++)
        {
            if (RamSlots[i].GetComponentInChildren<UIComponentSwap>().component != null)
            {
                Ram[RamInstalled] = RamSlots[i].GetComponentInChildren<UIComponentSwap>().component;
                RamInstalled += 1;
            }
        }
        for (int i = 0; i < CPUSlots.Length; i++)
        {
            if (CPUSlots[i].GetComponentInChildren<UIComponentSwap>().component != null)
            {
                CPU[CPUsInstalled] = CPUSlots[i].GetComponentInChildren<UIComponentSwap>().component;
                CPUsInstalled += 1;
            }
        }
        for (int i = 0; i < PSUSlots.Length; i++)
        {
            if (PSUSlots[i].GetComponentInChildren<UIComponentSwap>().component != null)
            {
                PSU[PSUInstalled] = PSUSlots[i].GetComponentInChildren<UIComponentSwap>().component;
                PSUInstalled += 1;
            }
        }


        if (GPUsInstalled > 0)
        {
            for (int i = 0; i < GPUsInstalled; i++)
            {
                GPU[i].transform.position = GPUSlots[i].transform.position;
            }
        }
        if (RamInstalled > 0)
        {
            for (int i = 0; i < RamInstalled; i++)
            {
                Ram[i].transform.position = RamSlots[i].transform.position;
                NooFBuffs += Ram[i].GetComponent<RamBase>().noOfBuffs;
            }
        }
        if (CPUsInstalled > 0)
        {
            for (int i = 0; i < CPUsInstalled; i++)
            {
                CPU[i].transform.position = CPUSlots[i].transform.position;
                StaminaMax += CPU[i].GetComponent<CPUBase>().StaminaMax;
                hpMax += CPU[i].GetComponent<CPUBase>().hpMax;
                shieldMax += CPU[i].GetComponent<CPUBase>().shieldMax;
                dashStamCost = CPU[i].GetComponent<CPUBase>().dashStamCost;
                staminaDrain = CPU[i].GetComponent<CPUBase>().staminaDrain;
                staminaRecovery += CPU[i].GetComponent<CPUBase>().staminaRecovery;
                hPRecovery += CPU[i].GetComponent<CPUBase>().hPRecovery;
                shieldRecovery += CPU[i].GetComponent<CPUBase>().shieldRecovery;
            }
        }
        if (PSUInstalled > 0)
        {
            for (int i = 0; i < PSUInstalled; i++)
            {
                PSU[i].transform.position = PSUSlots[i].transform.position;
                maxPower += PSU[i].GetComponent<PowerSupplyBase>().PowerSupplied;
            }
            currentPower = Mathf.Clamp(currentPower, 0, maxPower);
        }
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null)
            {
                Inventory[i].transform.position = InventorySpaces[i].transform.position;
            }
        }
        playerController.GetStats();
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
