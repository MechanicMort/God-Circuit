using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotInLine : MonoBehaviour
{
    public bool isLast;
    public GameObject nextInLine;
    // Start is called before the first frame update
    void Start()
    {
        if (nextInLine == null)
        {
            isLast = true;
        }
        else
        {
            isLast=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
