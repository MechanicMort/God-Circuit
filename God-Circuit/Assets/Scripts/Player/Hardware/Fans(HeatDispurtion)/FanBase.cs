using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBase : UIComponentSwap
{
    public float heatDispurtion;

    [Header("Component")]
    public string componentName;
    public string componentType;
    public float powerDraw;

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

    void FanSetUp()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
