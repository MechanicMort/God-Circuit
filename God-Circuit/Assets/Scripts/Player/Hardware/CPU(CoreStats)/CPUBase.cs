using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBase : UIComponentSwap
{

    [Header("Component")]
    public string componentName;
    public string componentType;
    public float powerDraw;
    public float heatGeneration;
    [Header("PlayerStats")]
    public float StaminaMax = 100;

    public float hpMax = 100;

    public float shieldMax = 100;

    public float airJumps = 1;
    public float airJumpsMax = 1;
    public float dashLength = 100;
    public float dashCoolDown = 100;
    public float dashStamCost;

    public float Stamina;
    public float staminaDrain;
    public float staminaRecovery;
    public float hP;
    public float hPRecovery;

    public float shield;
    public float shieldRecovery;

    public float speed;
    public float sprintSpeed = 16.5f;
    public float normalSpeed = 10.5f;
    public float creouchSpeed = 6.5f;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float drainwait = 100;


    void GetComponent(PassValues passValues)
    {
        if (passValues.expectedComponent == componentType)
        {
            print("Correct Slot");
            passValues.thisUICS.amICorrectType = true;
        }
        else
        {
            print("Component Type:  "+ componentType);
            print("passValues Component Type:  " + passValues.expectedComponent);
            print(passValues.expectedComponent+"   "+ componentType);
            print("WrongSlot");
        }

    }
}
