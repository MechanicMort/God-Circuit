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
    public GameObject[] Fans;
    public int FansInstalled;
    public GameObject OutPutDevice;
    public int OutPutDeviceInstalled;
    public GameObject VirusProtection;
    public int VirusProtectionInstalled;
    public GameObject projectile;

    [Header("BoardManagement")]
    public GameObject[] Inventory = new GameObject[6];
    public GameObject[] InventorySpaces = new GameObject[6];
    public GameObject[] GPUSlots;
    public GameObject[] CPUSlots;
    public GameObject[] RamSlots;
    public GameObject[] FanSlots;
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
    public float powerRegen;
    public float currentPower;
    public float powerPerShot;
    public float fireRate;

    [Header("Heat")]
    public float heatDispersion;
    public float totalHeat;
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
        StartCoroutine(Tickers());
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
        PSUInstalled = 0;
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
        for (int i = 0; i < FanSlots.Length; i++)
        {
            if (FanSlots[i].GetComponentInChildren<UIComponentSwap>().component != null)
            {
                Fans[FansInstalled] = FanSlots[i].GetComponentInChildren<UIComponentSwap>().component;
                FansInstalled += 1;
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



        if (FansInstalled > 0)
        {
            for (int i = 0; i < FansInstalled; i++)
            {
                if (i == 0)
                {
                    heatDispersion += Fans[i].GetComponent<FanBase>().heatDispurtion;
                }
                Fans[i].transform.position = FanSlots[i].transform.position;
            }
        }

        if (GPUsInstalled > 0)
        {
            for (int i = 0; i < GPUsInstalled; i++)
            {
                if (i==0)
                {
                    projectile = GPU[0].GetComponent<GPUBase>().projectile;
                    fireRate = GPU[0].GetComponent<GPUBase>().fireRate;
                }
                GPU[i].transform.position = GPUSlots[i].transform.position;
                powerPerShot += GPU[i].GetComponent<GPUBase>().powerDraw;
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
                powerRegen += PSU[i].GetComponent<PowerSupplyBase>().PowerRegen;
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
    private IEnumerator Tickers()
    {
        yield return new WaitForSeconds(0.01f);
        totalHeat -= heatDispersion;
        currentPower += powerRegen;
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
        totalHeat = Mathf.Clamp(totalHeat, 0, 100);
        StartCoroutine(Tickers());


    }


        public void FireWeapon( )
    {
        
        if(GPUsInstalled > 0)
        {
            print("Should Fire");
            if (currentPower > powerPerShot)
            {
                OutPutDevice.GetComponent<BasicRangedWeapon>().FireWeapon(projectile);
                DrainPower(powerPerShot);
            }

        }
    }

    public void AddPower(float power)
    {
        currentPower += power;
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
    }
}
