using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBase : MonoBehaviour
{
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

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float drainwait = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
