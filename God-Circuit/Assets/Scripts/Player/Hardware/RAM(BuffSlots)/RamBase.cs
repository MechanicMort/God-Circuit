using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamBase : UIComponentSwap
{
    public float noOfBuffs;
    public float powerDraw;
    public float heatGeneration;
    // Start is called before the first frame update
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
