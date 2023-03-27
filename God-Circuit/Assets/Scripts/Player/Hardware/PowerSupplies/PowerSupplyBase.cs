using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyBase : UIComponentSwap
{
    public float PowerSupplied;
    public float PowerRegen;

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

}
