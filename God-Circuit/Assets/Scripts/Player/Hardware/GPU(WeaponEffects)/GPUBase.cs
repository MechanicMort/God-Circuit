using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUBase : UIComponentSwap
{
    public float powerDraw;
    public float powerDrawPerShot;
    public float heatGeneration;
    public float fireRate;
    public GameObject motherBoard;
    public GameObject myWeapon;

    [Header("Component")]
    public string componentName;
    public string componentType;



    void GetComponent(PassValues passValues)
    {
        if (passValues.expectedComponent == componentType)
        {
            print("Correct Slot");
            passValues.thisUICS.amICorrectType = true;
        }
        else
        {
            print("WrongSlot");
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GPUSetUP()
    {
        motherBoard = transform.parent.gameObject;
        myWeapon = GameObject.FindGameObjectWithTag("CurrentWeapon");

    }

}
