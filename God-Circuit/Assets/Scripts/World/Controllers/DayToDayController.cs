using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayToDayController : MonoBehaviour
{

    public int iDay;

    public GameObject normanNB;
    public GameObject patrickNB;
    public GameObject kevinNB;
    // Start is called before the first frame update
    void Start()
    {
       iDay = PlayerPrefs.GetInt("iDay",0);
        if (iDay == 0)
        {
            //start of game
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
