using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICOmponentManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject selectedComponent;
    public MotherBoard MotherBoard;




    public void SelectObject(GameObject objectToSelect)
    {
        objectToSelect = selectedComponent;
    }

    
    public void ComponentSwapped()
    {
        MotherBoard.PartSwap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
